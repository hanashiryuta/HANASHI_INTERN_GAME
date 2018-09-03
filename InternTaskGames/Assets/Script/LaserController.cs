///
///製作日：2018/08/31
///作成者：葉梨竜太
///レーザーポインター表示クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

    //レーザー始点
    public GameObject startPosition;
    //レーザー終点
    public GameObject endPosition;
    //ラインレンダラー
    LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
        //ラインレンダラー取得
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //始点設定
        lineRenderer.SetPosition(0, startPosition.transform.position);
        //終点設定
        lineRenderer.SetPosition(1, endPosition.transform.position);
	}
}
