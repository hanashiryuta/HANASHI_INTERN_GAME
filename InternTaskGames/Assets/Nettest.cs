using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Nettest : NetworkBehaviour {

    float T = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        T -= Time.deltaTime;
        if (T <= 0)
        {
            if (isLocalPlayer)
                NetworkManager.singleton.StopHost();
        }
	}
}
