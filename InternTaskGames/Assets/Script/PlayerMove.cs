///
///製作日：2018/08/29
///作成者：葉梨竜太
///プレイヤーの動きクラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour {

    void Update()
    {
        if (IsNetwork.isOnline)
        {
            //当たった位置との角度計算
            float angle = Mathf.Atan2(transform.position.z - 0, transform.position.x - 0) * 180 / Mathf.PI;
            //ラケットを回転
            transform.rotation = Quaternion.Euler(0, angle - 90, 0);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        //爆弾に当たったら
        if (col.gameObject.CompareTag("Bomb"))
        {
            if (!IsNetwork.isOnline)
            {
                //スコア初期化
                ComboController.ComboInit();
                //爆弾消滅
                col.gameObject.GetComponent<BombMove>().bombState = BombState.DEATH;
            }
            else
            {
                GameObject.Find(gameObject.name + "Score").GetComponent<OnlineScore>().CmdOnlineScoreAdd();
                //爆弾消滅
                col.gameObject.GetComponent<BombMove>().bombState = BombState.DEATH;
            }
        }
    }
}
