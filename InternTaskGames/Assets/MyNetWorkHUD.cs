using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetWorkHUD : MonoBehaviour{

    MyNetworkDiscover networkDiscover;

    // Use this for initialization
    void Start()
    {
        networkDiscover = GetComponent<MyNetworkDiscover>();
    }

    //部屋を作成する場合
    public void OnCreatedRoom()
    {
        NetworkManager.singleton.StartHost();
        networkDiscover.Initialize();
        networkDiscover.StartAsServer();
    }

    //部屋に所属する場合
    public void OnJoinedRoom()
    {
        networkDiscover.Initialize();
        networkDiscover.StartAsClient();
    }
}
