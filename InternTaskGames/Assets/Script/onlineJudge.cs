using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onlineJudge : MonoBehaviour {

    public OnlineScore player1Score;
    public OnlineScore player2Score;

    public TimeController gameTime;

    public Text winPlayerNameText;

    float showTime = 4.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(gameTime.isTimeEnd)
        {
            gameTime.gameObject.SetActive(false);
            winPlayerNameText.enabled = true;
            if (player1Score.onlineScore > player2Score.onlineScore)
                winPlayerNameText.text = "Player2Win!";
            else if (player1Score.onlineScore < player2Score.onlineScore)
                winPlayerNameText.text = "Player1Win!";
            else
                winPlayerNameText.text = "Draw!";


                showTime -= Time.deltaTime;
            if (showTime <= 0)
                FadeController.isSceneEnd = true;
        }
        else
        {
            winPlayerNameText.enabled = false;
        }
	}
}
