﻿///
///製作日：2018/08/27
///作成者：葉梨竜太
///エネミーの消滅処理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

    //消滅するかどうか
    [HideInInspector]
    public bool isDeath = false;
    //消滅までの時間
    public float deathTime = 5.0f;
    //アニメーター
    public Animator anim;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //消滅可能なら消滅までの時間を減らしていく
        if(isDeath)
            deathTime -= Time.deltaTime;
        //消滅までの時間が0になったら（時間が来たら）
        if (deathTime <= 0)
        {
            //アニメ時間取得
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0); // layerNo:Base Layer == 0 

            //アニメ再生終了なら
            if (stateInfo.normalizedTime >= 1.0f)
            {
                //オブジェクト消滅
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        //爆弾に当たったら
        if(col.gameObject.CompareTag("Bomb"))
        {
            //爆弾の状態が返され中なら
            //if (col.gameObject.GetComponent<BombMove>().bombState == BombState.RETURNMOVE)
            if(col.gameObject.GetComponent<BombMove>().targetObject == gameObject)
            {
                //アニメ再生
                anim.SetTrigger("Death");
                //スコア加算
                ScoreController.ScoreAdd(100);
                //爆弾消滅
                col.gameObject.GetComponent<BombMove>().bombState = BombState.DEATH;
                //消滅時間を0に
                deathTime = 1;
            }
        }
    }
}
