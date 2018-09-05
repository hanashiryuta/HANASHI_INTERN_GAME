using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSet : NetworkBehaviour
{
    public List<GameObject> objList;

    public List<Behaviour> behavList;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            foreach (var cx in objList)
            {
                cx.SetActive(false);
            }
            foreach(var cx in behavList)
            {
                cx.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
