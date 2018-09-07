///
///製作日：2018/09/03
///作成者：葉梨竜太
///カウントダウン管理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

/// <summary>
/// カウントダウン状態
/// </summary>
public enum CountDownState
{
    NUMBER,//数値
    START,//”Start”表示
    END//終了
}

public class CountDownController : NetworkBehaviour {

    //カウントダウン表示テキスト
    public Text countDownText;
    //カウントダウン時間
    [SyncVar]
    float countDownTime = 3;
    //カウントダウン状態
    [HideInInspector]
    public CountDownState countDownState = CountDownState.NUMBER;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (IsNetwork.isOnline)
            OnlineUpdate();
        else
            OfflineUpdate();
	}

    private void OfflineUpdate()
    {
        //フェード状態が待機状態なら
        if (FadeController.fadeActionState == FadeActionState.Stay)
            //カウントダウンを減らす
            countDownTime -= Time.deltaTime;
        //カウントダウン状態で処理変更
        switch (countDownState)
        {
            //数値表示
            case CountDownState.NUMBER:
                //小数点以下切り上げで表示
                countDownText.text = Mathf.Ceil(countDownTime).ToString("0");
                //時間が来たら
                if (countDownTime <= 0)
                {
                    //1秒に設定
                    countDownTime = 1.0f;
                    //状態遷移
                    countDownState = CountDownState.START;
                }
                break;
            //”Start”表示
            case CountDownState.START:
                //Start表示
                countDownText.text = "Start!";
                //時間が来たら
                if (countDownTime <= 0)
                {
                    //非表示
                    countDownText.enabled = false;
                    //状態遷移
                    countDownState = CountDownState.END;
                }
                break;
        }
    }

    private void OnlineUpdate()
    {
        if (NetworkManager.singleton.numPlayers <= 1)
            return;

        //フェード状態が待機状態なら
        if (FadeController.fadeActionState == FadeActionState.Stay&&isServer)
            //カウントダウンを減らす
            countDownTime -= Time.deltaTime;
        //カウントダウン状態で処理変更
        switch (countDownState)
        {
            //数値表示
            case CountDownState.NUMBER:
                //小数点以下切り上げで表示
                countDownText.text = Mathf.Ceil(countDownTime).ToString("0");
                //時間が来たら
                if (countDownTime <= 0)
                {
                    //1秒に設定
                    countDownTime = 1.0f;
                    //状態遷移
                    countDownState = CountDownState.START;
                }
                break;
            //”Start”表示
            case CountDownState.START:
                //Start表示
                countDownText.text = "Start!";
                //時間が来たら
                if (countDownTime <= 0)
                {
                    //非表示
                    countDownText.enabled = false;
                    //状態遷移
                    countDownState = CountDownState.END;
                }
                break;
        }
    }
}
