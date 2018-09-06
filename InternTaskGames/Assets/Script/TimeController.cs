///
///製作日：2018/08/29
///作成者：葉梨竜太
///ゲーム時間管理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour {

    //時間表示オブジェクト
    public GameObject timeTextObj;
    //時間表示テキスト
    Text timeText;
    //ゲーム時間
    public float gameTime = 60.00f;
    //時間処理開始するか
    //public static bool isTimeStart;
    CountDownController countDownController;

	// Use this for initialization
	void Start () {
        //isTimeStart = false;
        //時間表示テキスト取得
        timeText = timeTextObj.GetComponent<Text>();
        countDownController = GameObject.Find("CountDownUI").GetComponent<CountDownController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (countDownController.countDownState == CountDownState.END)
        {
            timeText.enabled = true;
            //時間表示
            timeText.text = "Time:" + gameTime.ToString("00.00");
            //時間処理が開始できるなら

            //ゲーム時間を減らす
            gameTime -= Time.deltaTime;

            //ゲーム時間が0になったら
            if (gameTime <= 0)
            {
                gameTime = 0;
                //フェードアウト開始
                GameObject.Find("Fade").GetComponent<FadeController>().isSceneEnd = true;
            }
        }
        else
        {
            timeText.enabled = false;
        }
	}
}
