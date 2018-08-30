///
///製作日：2018/08/28
///作成者：葉梨竜太
///エネミーの爆弾投擲クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombThrow : MonoBehaviour
{
    //壁の種類
    [HideInInspector]
    public WallType wallType = WallType.FRONTWALL;

    //基礎爆弾
    public GameObject originBomb;
    //投擲までの時間
    float throwTime = 2.0f;
    //エネミーの消滅処理クラス
    EnemyDeath enemyDeath;
    //ターゲットオブジェクト
    GameObject targetObject;
    public float angle = 30;

    public static bool isThrow;
    
	// Use this for initialization
	void Start () {
        isThrow = false;
        //クラス取得
        enemyDeath = GetComponent<EnemyDeath>();
        //ターゲット検索
        targetObject = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
            //射出までの時間を減らしていく
            throwTime -= Time.deltaTime;
        //射出までの時間が0になったら（時間が来たら）、かつ
        //エネミーが消滅状態でなければ
        if(throwTime <= 0&&!enemyDeath.isDeath)
        {
            //爆弾生成
            GameObject bomb = Instantiate(originBomb, transform.position, Quaternion.identity);
            //爆弾投擲
            BombThrow(bomb, angle,bomb.GetComponent<BombMove>().Grav.y);
            //壁の種類を伝える
            bomb.GetComponent<BombMove>().wallType = wallType;
            //自身を渡す
            bomb.GetComponent<BombMove>().enemy = this.gameObject;
            //エネミー消滅状態に
            enemyDeath.isDeath = true;
        }
	}

    /// <summary>
    /// 爆弾を投げるメソッド
    /// </summary>
    /// <param name="bomb">爆弾</param>
    /// <param name="angle">角度</param>
    void BombThrow(GameObject bomb,float angle,float Grav)
    {
        //ターゲットポジション設定
        Vector3 targetPosition = targetObject.transform.position;

        //初速計算
        float v0 = V0Culculater(transform.position, targetPosition, angle,Grav);
        //0以下なら投げない
        if (v0 <= 0)
        {
            Destroy(bomb);
            return;
        }
        
        //ベクトル計算
        Vector3 velocity = VectorToVector3(v0, angle, targetPosition);

        //射出する力計算　力=速度ベクトル*重さ
        Vector3 force = velocity * bomb.GetComponent<Rigidbody>().mass;
        
        //射出
        bomb.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
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
        if(v0Spuare <= 0.0f)
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
    Vector3 VectorToVector3(float v0,float angle,Vector3 target)
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
