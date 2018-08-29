///
///製作日：2018/08/28
///作成者：葉梨竜太
///爆弾の消滅処理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDeath : MonoBehaviour
{
    //消滅までの時間
    public float deathTime = 5.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deathTime -= Time.deltaTime;
        //消滅までの時間が0になったら（時間が来たら）
        if (deathTime <= 0)
        {
            //オブジェクト消滅
            Destroy(gameObject);
        }
    }
}
