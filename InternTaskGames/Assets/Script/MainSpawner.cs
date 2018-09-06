///
///製作日：2018/09/06
///作成者：葉梨竜太
///メインシーンオブジェクト生成クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawner : MonoBehaviour {

    //オブジェクトリスト
    public List<GameObject> objList;

	// Use this for initialization
	void Start () {
        //リストすべて
        foreach(var cx in objList)
        {
            //オブジェクト生成
            GameObject obj = Instantiate(cx);
            //名前から(Clone)を外す
            obj.name = obj.name.Replace("(Clone)", "");
        }		
	}
}
