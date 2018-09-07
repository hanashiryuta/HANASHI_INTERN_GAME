using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineScore : MonoBehaviour {

    [HideInInspector]
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

    public void OnlineScoreAdd()
    {
        onlineScore++;
    }

    public float OnlineScoreReturn()
    {
        return onlineScore;
    }
}
