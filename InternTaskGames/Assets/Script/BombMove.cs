///
///製作日：2018/09/05
///作成者：葉梨竜太
///爆弾移動クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 爆弾の状態
/// </summary>
public enum BombState
{
    NORMAL,//通常状態
    THROWSET,//投げ始め
    STRAIGHT,//直線
    DEATH//死亡状態
}

public class BombMove : NetworkBehaviour
{
    //爆弾状態
    [HideInInspector]
    public BombState bombState = BombState.NORMAL;
    //自身を投げたエネミー
    [HideInInspector]
    //public GameObject enemy;
    public GameObject originObject;
    //投擲元の壁
    [HideInInspector]
    public WallType wallType = WallType.FRONTWALL;

    //リジッドボディ
    Rigidbody rigid;
    //重力
    public Vector3 Grav = new Vector3(0, 9.8f, 0);
    //目標地点
    [HideInInspector]
    public GameObject targetObject;
    //移動量
    Vector3 velocity = Vector3.zero;
    //跳ね返りスピード
    public float returnSpeed = 000.1f;
    //壁管理クラス
    WallController wallController;
    //消滅までの時間
    public float deathTime = 5.0f;
    //爆発パーティクル
    public GameObject explosionParticle;
    //爆発SEオブジェクト
    public GameObject explosionSE;
    //射出角度
    float angle = 0;

    // Use this for initialization
    void Start()
    {
        //壁管理クラス取得
        wallController = GameObject.Find("Walls").GetComponent<WallController>();
        //リジッドボディ取得
        rigid = GetComponent<Rigidbody>();
        //壁管理クラスに自身を保存
        wallController.CmdAddBombs(this.gameObject, wallType);
    }

    // Update is called once per frame
    void Update()
    {
        //オンラインかオフラインで対応を変える
        if (IsNetwork.isOnline)
            OnlineUpdate();
        else
            OfflineUpdate();

    }

    /// <summary>
    /// オンライン時のアップデート
    /// </summary>
    [ServerCallback]
    void OnlineUpdate()
    {
        //自身の状態によって行動が変化する
        switch (bombState)
        {
            //通常状態
            case BombState.NORMAL:
                //通常動作
                BombNormal();
                //サーバーに位置情報送信
                CmdMoveBomb(transform.position);
                break;

            //投げ始め
            case BombState.THROWSET:
                //移動量を0にする
                rigid.velocity = Vector3.zero;
                //爆弾投擲
                BombFly(targetObject);
                break; 
                
            //死亡時               
            case BombState.DEATH:
                //爆弾死亡処理
                BombDeath();
                //オンライン上でのオブジェクト消滅
                NetworkServer.Destroy(gameObject);
                break;
        }
    }

    /// <summary>
    /// オフライン上でのアップデート
    /// </summary>
    void OfflineUpdate()
    {
        //自身の状態によって行動が変化する
        switch (bombState)
        {
            //通常状態
            case BombState.NORMAL:
                //通常動作
                BombNormal();
                break;

            //投げ始め
            case BombState.THROWSET:
                //移動量を0にする
                BombFly(targetObject);
                break;

            //直線
            case BombState.STRAIGHT:
                //移動量設定
                velocity = (targetObject.transform.position - transform.position).normalized;
                //ランダム回転
                transform.Rotate(new Vector3(Random.Range(0, 180),
                                             Random.Range(0, 180),
                                             Random.Range(0, 180)
                                            ) * Time.deltaTime * 2);
                //ターゲットに向かって移動
                transform.position += velocity * 0.5f;
                break;

            //死亡時
            case BombState.DEATH:
                //爆弾死亡処理
                BombDeath();
                //オブジェクト消滅
                Destroy(gameObject);
                break;
        }
    }

    /// <summary>
    /// 通常動作
    /// </summary>
    void BombNormal()
    {
        //ランダム回転しながら飛んでいく
        transform.Rotate(new Vector3(Random.Range(0, 180),
                                     Random.Range(0, 180),
                                     Random.Range(0, 180)
                                    ) * Time.deltaTime);
        //自分で設定した重力を加える
        rigid.AddForce(-Grav, ForceMode.Acceleration);

        deathTime -= Time.deltaTime;
        //消滅までの時間が0になったら（時間が来たら）
        if (deathTime <= 0)
        {
            //オブジェクト消滅状態に
            bombState = BombState.DEATH;
        }
    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    void BombDeath()
    {
        //爆発SEオブジェクト生成
        Instantiate(explosionSE, transform.position, Quaternion.identity);
        //爆発パーティクル生成
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        //壁管理クラスから自身を除外
        wallController.CmdRemoveBombs(this.gameObject, wallType);
    }

    /// <summary>
    /// ターゲット変更
    /// </summary>
    /// <param name="newOrigin"></param>
    public void TargetChange(GameObject newOrigin)
    {
        //移動量を0に
        rigid.velocity = Vector3.zero;
        //死亡時間初期化
        deathTime = 10.0f;
        //投擲元をターゲットに
        targetObject = originObject;
        //新しい投擲元を追加
        originObject = newOrigin;
        //目標がオフラインのエネミーだったら
        if (targetObject.CompareTag("Enemy")&&!IsNetwork.isOnline)
        {
            //直線移動
            bombState = BombState.STRAIGHT;
        }
        else
        {
            //投げ返し
            bombState = BombState.THROWSET;
        }
    }
    
    /// <summary>
     /// 爆弾を投げるメソッド
     /// </summary>
     /// <param name="bomb">爆弾</param>
     /// <param name="angle">角度</param>
    void BombFly(GameObject targetObj)
    {
        //角度設定
        angle = Random.Range(30, 60);

        //ターゲットポジション設定
        Vector3 targetPosition = targetObj.transform.position;

        //初速計算
        float v0 = V0Culculater(transform.position, targetPosition, angle, Grav.y);
        //0以下なら投げない
        if (v0 <= 0)
        {
            Destroy(gameObject);
            return;
        }

        //ベクトル計算
        Vector3 velocity = VectorToVector3(v0, angle, targetPosition);

        //射出する力計算　力=速度ベクトル*重さ
        Vector3 force = velocity * GetComponent<Rigidbody>().mass;

        //射出
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        
        bombState = BombState.NORMAL;
    }

    /// <summary>
    /// サーバーへ位置送信
    /// </summary>
    /// <param name="position"></param>
    [Command]
    void CmdMoveBomb(Vector3 position)
    {
        //サーバーに接続しているクライアント
        foreach (var conn in NetworkServer.connections)
        {
            //つながっていないやつには渡さない
            if (conn == null || !conn.isReady)
                continue;
            //自身にも渡さない
            if (conn == connectionToClient)
                continue;

            //サーバーから位置送信
            TargetSyncTransform(conn, position);
        }
    }

    /// <summary>
    /// サーバーから位置送信
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="position"></param>
    [TargetRpc]
    void TargetSyncTransform(NetworkConnection conn, Vector3 position)
    {
        //位置更新
        transform.position = position;
    }

    /// <summary>
    /// 斜方投射の初速度を求める
    /// </summary>
    /// <param name="start">投擲地点</param>
    /// <param name="target">目標</param>
    /// <param name="angle">角度</param>
    /// <returns></returns>
    float V0Culculater(Vector3 start, Vector3 target, float angle, float Grav)
    {
        //xz平面での距離を求める
        Vector2 startPos = new Vector2(start.x, start.z);
        Vector2 targetPos = new Vector2(target.x, target.z);
        float distance = Vector2.Distance(targetPos, startPos);

        //射出地点から目標までのx距離
        float x = distance;
        //重力加速度
        float g = -Grav;
        //初期y座標
        float y0 = start.y;
        //目標のy座標
        float y = target.y;

        //角度をラジアン変換
        float rad = angle * Mathf.PI / 180;

        //コサインとタンジェントを求める
        float cos = Mathf.Cos(rad);
        float tan = Mathf.Tan(rad);

        //斜方投射の式
        /*

        x成分の速度
        vx = v0cosθ　一定
        これより↓
        x成分の位置
        x = v0cosθ*t

        y成分の速度
        vy = v0sinθ-gt
        これより↓
        y成分の位置
        y = v0sinθ*t - 1/2*gt^2

        xの位置の式をyの位置の式に代入し、tを消すと
        斜方投射の物体の軌道を表す式が出る
        y = tanθ*x - g*x^2/2*v0^2*cos^2θ

      　 斜方投射の式を初速度の式に変換
        そこに必要な値を入れる*/
        float v0Spuare = g * x * x / (2 * cos * cos * (y - y0 - x * tan));

        //負数の場合計算できない
        if (v0Spuare <= 0.0f)
        {
            return 0.0f;
        }
        //平方根の計算
        float v0 = Mathf.Sqrt(v0Spuare);
        return v0;
    }

    /// <summary>
    /// 初速度よりvector3を求める
    /// </summary>
    /// <param name="v0">初速度</param>
    /// <param name="angle">角度</param>
    /// <param name="target">目標</param>
    /// <returns></returns>
    Vector3 VectorToVector3(float v0, float angle, Vector3 target)
    {
        //投擲地点
        Vector3 startPos = transform.position;
        //目標地点
        Vector3 targetPos = target;
        //y座標を0に
        startPos.y = 0;
        targetPos.y = 0;

        //投擲地点から目標までの方向
        Vector3 dir = (targetPos - startPos).normalized;
        //投擲方向y軸回転
        Quaternion yawRot = Quaternion.FromToRotation(Vector3.right, dir);
        //投擲ベクトルの大きさ
        Vector3 vec = v0 * Vector3.right;

        //y軸回転*z軸回転*大きさ
        vec = yawRot * Quaternion.AngleAxis(angle, Vector3.forward) * vec;

        return vec;
    }
}