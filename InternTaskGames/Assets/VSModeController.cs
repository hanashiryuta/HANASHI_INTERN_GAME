using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class VSModeController : NetworkBehaviour {

    float time;

	// Use this for initialization
	void Start () {
		time = Random.Range(10, 15);
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            // 切断は、サーバーもクライアントもStopHost()でOK
            NetworkManager.singleton.StopHost();
        }
	}
}
