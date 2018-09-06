///
///製作日：2018/09/06
///作成者：葉梨竜太
///改変ネットワークマネージャー
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetWorkManager : NetworkManager {

    //改変Ip取得クラス
    MyNetworkDiscover networkDiscover;

	// Use this for initialization
	void Start () {
        //改変Ip取得クラス取得
        networkDiscover = GetComponent<MyNetworkDiscover>();
	}

    // Update is called once per frame
    void Update()
    {
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.A))
        {
            NetworkManager.singleton.StartHost();
            networkDiscover.Initialize();
            networkDiscover.StartAsServer();
        }
    }
    
    /// <summary>
    /// サーバーが停止したとき
    /// </summary>
    public override void OnStopServer()
    {
        //ブロードキャストやめる
        networkDiscover.StopBroadcast();
    }
}
