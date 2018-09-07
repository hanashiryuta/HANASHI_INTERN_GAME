///
///製作日：2018/08/29
///作成者：葉梨竜太
///コンボ管理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboController : MonoBehaviour {

    //コンボ数
    public static int comboCount;
    //コンボ数表示テキスト
    public Text[] comboTexts;
    //カウントダウンクラス
    CountDownController countDownController;

    // Use this for initialization
    void Start () {
        //コンボ数初期化
        ComboInit();
        //カウントダウンクラス取得
        countDownController = GameObject.Find("CountDownUI").GetComponent<CountDownController>();
    }
	
	// Update is called once per frame
	void Update () {
		foreach(var cx in comboTexts)
        {
            //カウントダウンが終わっていたら
            if (countDownController.countDownState == CountDownState.END)
            {
                //表示
                cx.enabled = true;
                //コンボ数が0以下なら
                if (comboCount <= 0)
                    //何も表示しない
                    cx.text = "";
                //それ以外なら
                else
                    //コンボ数表示
                    cx.text = comboCount + "Combo!";
            }
            else
            {
                //非表示
                cx.enabled = false;
            }
        }
	}

    /// <summary>
    /// コンボ数返す
    /// </summary>
    /// <returns></returns>
    public static int ReturnCombo()
    {
        return comboCount;
    }

    /// <summary>
    /// コンボ数増加
    /// </summary>
    public static void ComboAdd()
    {
        comboCount++;
    }

    /// <summary>
    /// コンボ数初期化
    /// </summary>
    public static void ComboInit()
    {
        comboCount = 0;
    }
}
