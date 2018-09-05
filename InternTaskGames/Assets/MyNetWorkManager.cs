using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetWorkManager : NetworkManager {

    MyNetworkDiscover networkDiscover;

	// Use this for initialization
	void Start () {
        networkDiscover = GetComponent<MyNetworkDiscover>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A))
        {
            NetworkManager.singleton.StartHost();
            networkDiscover.Initialize();
            networkDiscover.StartAsServer();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            singleton.StopHost();
        }
	}

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player;
        Transform startPos = GetStartPosition();
        if (startPos != null)
            player = (GameObject)Instantiate(playerPrefab, startPos.position, startPos.rotation);
        else
            player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnStopServer()
    {
        networkDiscover.StopBroadcast();
    }
}
