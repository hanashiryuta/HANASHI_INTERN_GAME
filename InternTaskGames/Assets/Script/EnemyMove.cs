///
///製作日：2018/08/28
///作成者：葉梨竜太
///エネミーの動きクラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //カメラへの方向ベクトル取得
        Vector3 diff = GameObject.FindGameObjectWithTag("MainCamera").transform.position - transform.position;
        //y軸は回転させない
        diff.y = 0;
        //方向の絶対値が0以上なら
        if (diff.magnitude > 0.01f)
        {
            //自身を回転
            transform.rotation = Quaternion.LookRotation(diff);
        }
    }
}
