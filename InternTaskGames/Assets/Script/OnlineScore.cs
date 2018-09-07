using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class OnlineScore : NetworkBehaviour {

    [HideInInspector,SyncVar]
    public float onlineScore;

    Text onlineScoreText;

	// Use this for initialization
	void Start () {
        onlineScoreText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        onlineScoreText.text = onlineScore.ToString("000");
    }

    [Command]
    public void CmdOnlineScoreAdd()
    {
        onlineScore++;
    }

    public float OnlineScoreReturn()
    {
        return onlineScore;
    }
}
