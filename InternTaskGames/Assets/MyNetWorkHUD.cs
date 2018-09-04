using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetWorkHUD : MonoBehaviour {

    //部屋を作成する場合
    public void OnCreatedRoom()
    {
        NetworkManager.singleton.StartHost();
    }

    //部屋に所属する場合
    public void OnJoinedRoom()
    {
        NetworkManager.singleton.networkAddress = "192.168.1.243";
        NetworkManager.singleton.StartClient();
    }
}
