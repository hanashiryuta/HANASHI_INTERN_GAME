///
///製作日：2018/08/28
///作成者：葉梨竜太
///ラケット管理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour {

    //跳ね返しSE
    public AudioClip sieldSE;
    //オーディオソース
    AudioSource audioSource;

    string target;

    void Start()
    {
        //オーディオソース取得
        audioSource = GetComponent<AudioSource>();
        if (transform.name == "Player1")
            target = "Player2";
        else
            target = "Player1";

    }

    void OnTriggerEnter(Collider col)
    {
        //爆弾に当たったら
        if (col.gameObject.CompareTag("Bomb"))
        {
            //SE再生
            audioSource.PlayOneShot(sieldSE);
            //スコア加算
            ScoreController.ScoreAdd(100);
            //コンボ加算
            ComboController.ComboAdd();
            if (!IsNetwork.isOnline)
                //爆弾状態変更
                col.gameObject.GetComponent<BombMove>().TargetChange(gameObject);
            else
                col.gameObject.GetComponent<BombMove>().TargetChange(gameObject, GameObject.Find(target));
        }
    }
}
