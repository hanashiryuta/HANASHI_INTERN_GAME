///
///製作日：2018/08/28
///作成者：葉梨竜太
///爆弾の動きクラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 爆弾の状態
/// </summary>
public enum BombState
{
    NORMAL,//通常状態
    RETURNSET,//返され始め
    RETURNMOVE,//返され中
}

public class BombMove : MonoBehaviour {

    //爆弾状態
    [HideInInspector]
    public BombState bombState = BombState.NORMAL;
    //自身を投げたエネミー
    [HideInInspector]
    public GameObject enemy;
    //投擲元の壁
    [HideInInspector]
    public WallType wallType = WallType.FRONTWALL;

    //リジッドボディ
    Rigidbody rigid;
    //重力
    public Vector3 Grav = new Vector3(0,9.8f,0);
    //壁の位置
    GameObject[] wallPoint;
    //目標地点
    Vector3 targetPosition;
    //移動量
    Vector3 velocity = Vector3.zero;
    //跳ね返りスピード
    public float returnSpeed = 000.1f;
    //壁管理クラス
    WallController wallController;

    // Use this for initialization
    void Start () {
        //壁管理クラス取得
        wallController = GameObject.Find("Walls").GetComponent<WallController>();
        //壁の位置取得
        wallPoint = wallController.wallPoint;
        //リジッドボディ取得
        rigid = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void FixedUpdate() {
        //自身の状態によって行動が変化する
        switch (bombState)
        {
            //通常状態
            case BombState.NORMAL:
                //自分で設定した重力を加える
                rigid.AddForce(-Grav, ForceMode.Acceleration);
                break;

            //返され始め
            case BombState.RETURNSET:
                //移動量を0にする
                rigid.velocity = Vector3.zero;
                //ターゲットを壁の位置に設定
                targetPosition = wallPoint[(int)wallType].transform.position;
                //自分を投げたエネミーがまだいたら
                if (enemy != null)
                    //エネミーに向かって飛んでいく
                    targetPosition = enemy.transform.position;
                //移動量を設定
                velocity = (targetPosition - transform.position).normalized;
                //レイヤー設定
                gameObject.layer = LayerMask.NameToLayer("ReturnBomb");
                //状態遷移
                bombState = BombState.RETURNMOVE;
                break;

            //返され中
            case BombState.RETURNMOVE:
                //ターゲットに向かって移動
                transform.position += velocity * returnSpeed;
                
                break;
        }
    }
}
