﻿///
///製作日：2018/08/28
///作成者：葉梨竜太
///ラケットの位置ポイントクラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketRangePointer : MonoBehaviour {

    //ラケット
    public GameObject racket;
    //レイヤー
    public LayerMask layerMask;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //コントローラからレイを飛ばす
        Ray pointer = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        //レイが指定したレイヤーを持つオブジェクトに当たったら
        if (Physics.Raycast(pointer, out hit, 100, layerMask))
        {
            //その位置にラケット配置
            racket.transform.position = hit.point;
        }

        //当たった位置との角度計算
        float angle = Mathf.Atan2(racket.transform.position.z - transform.position.z, racket.transform.position.x - transform.position.x) * 180/Mathf.PI;

        //ラケットを回転
        racket.transform.rotation = Quaternion.Euler(0, -angle+90, 0);

	}
}
