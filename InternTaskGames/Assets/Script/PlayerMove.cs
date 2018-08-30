///
///製作日：2018/08/29
///作成者：葉梨竜太
///プレイヤーの動きクラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        //爆弾に当たったら
        if (col.gameObject.CompareTag("Bomb"))
        {
            //スコア初期化
            ComboController.ComboInit();
            //爆弾消滅
            col.gameObject.GetComponent<BombMove>().bombState = BombState.DEATH;
        }
    }
}
