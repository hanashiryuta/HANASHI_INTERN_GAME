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

    void Start()
    {
        //オーディオソース取得
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision col)
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
            //爆弾状態変更
            col.gameObject.GetComponent<BombMove>().bombState = BombState.RETURNSET;
        }
    }
}
