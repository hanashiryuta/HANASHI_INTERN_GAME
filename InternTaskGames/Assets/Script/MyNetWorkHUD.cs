///
///製作日：2018/09/06
///作成者：葉梨竜太
///ネットワーク接続UI
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetWorkHUD : MonoBehaviour{

    //改変Ip取得クラス
    MyNetworkDiscover networkDiscover;

    // Use this for initialization
    void Start()
    {
        //改変Ip取得クラス
        networkDiscover = GetComponent<MyNetworkDiscover>();
    }

    //部屋を作成する場合
    public void OnCreatedRoom()
    {
        //ホスト起動
        NetworkManager.singleton.StartHost();
        //改変Ip取得クラス初期化
        networkDiscover.Initialize();
        //改変Ip取得クラスサーバーとしてスタート
        networkDiscover.StartAsServer();
    }

    //部屋に所属する場合
    public void OnJoinedRoom()
    {
        //改変Ip取得クラス初期化
        networkDiscover.Initialize();
        //改変Ip取得クラスクライアントとしてスタート
        networkDiscover.StartAsClient();
    }
}
