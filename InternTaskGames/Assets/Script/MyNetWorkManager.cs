///
///製作日：2018/09/06
///作成者：葉梨竜太
///改変ネットワークマネージャー
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MyNetWorkManager : NetworkManager {

    //改変Ip取得クラス
    MyNetworkDiscover networkDiscover;

    int id = 1;

	// Use this for initialization
	void Start () {
        //改変Ip取得クラス取得
        networkDiscover = GetComponent<MyNetworkDiscover>();
        //シーン読み込み時処理
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.A))
        {
            //改変Ip取得クラス初期化
            networkDiscover.Initialize();
            //改変Ip取得クラスクライアントとしてスタート
            networkDiscover.StartAsClient();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            //ホスト起動
            NetworkManager.singleton.StartHost();
            //改変Ip取得クラス初期化
            networkDiscover.Initialize();
            //改変Ip取得クラスサーバーとしてスタート
            networkDiscover.StartAsServer();
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject pPrefab = playerPrefab;

        GameObject player;

        player = (GameObject)Instantiate(pPrefab, GetStartPosition().position, Quaternion.identity);
        player.name = "Player" + id;
        id++;
        player.transform.Find("TrackingSpace/CenterEyeAnchor/Fade").GetComponent<FadeController>().nextScene = "Lobby";

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    /// <summary>
    /// サーバーが停止したとき
    /// </summary>
    public override void OnStopServer()
    {
        //ブロードキャストやめる
        networkDiscover.StopBroadcast();
    }
    
    /// <summary>
    /// シーン読み込み時処理
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //読み込んだシーンによってBGM切り替え
        switch (scene.name)
        {
            case "VSMode":
                break;
            default:
                id = 1;
                break;
        }
    }
}
