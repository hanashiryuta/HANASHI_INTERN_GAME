using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class onlineJudge : NetworkBehaviour {

    public OnlineScore player1Score;
    public OnlineScore player2Score;

    public TimeController gameTime;

    public Text winPlayerNameText;

    float showTime = 4.0f;

    [SyncVar]
    string winText = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
            if (gameTime.isTimeEnd)
            {
                gameTime.gameObject.SetActive(false);
                winPlayerNameText.enabled = true;
            if (isServer)
            {
                if (player1Score.onlineScore > player2Score.onlineScore)
                    winText = "Player1Win!";
                else if (player1Score.onlineScore < player2Score.onlineScore)
                    winText = "Player2Win!";
                else
                    winText = "Draw!";
            }


                showTime -= Time.deltaTime;
                if (showTime <= 0)
                    FadeController.isSceneEnd = true;
            }
            else
            {
                winPlayerNameText.enabled = false;
            }
        winPlayerNameText.text = winText;
       }

	}

