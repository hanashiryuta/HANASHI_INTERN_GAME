///
///製作日：2018/08/29
///作成者：葉梨竜太
///スコア管理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    //スコア
    static float score;
    //スコア倍率
    static float scoreMagni;
    //スコア表示オブジェクト
    public GameObject scoreTextObj;
    //スコア表示テキスト
    Text scoreText;
    //スコア倍率表示オブジェクト
    public GameObject magniTextObj;
    //スコア倍率表示テキスト
    Text magniText;

    // Use this for initialization
    void Start () {
        //スコア表示テキスト取得
        scoreText = scoreTextObj.GetComponent<Text>();
        //スコア初期化
        score = 0;
        //スコア倍率初期化
        scoreMagni = 1;
        //スコア倍率表示テキスト取得
        magniText = magniTextObj.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //コンボ数取得
        float comboCount = (float)ComboController.ReturnCombo();
        //スコア倍率設定
        scoreMagni = (1 + comboCount / 10);
        //スコア表示
        scoreText.text = "Score:" + score.ToString("00000");
        //スコア倍率が0以下なら
        if (scoreMagni <= 0)
            //表示しない
            magniText.text = "";
        //それ以外なら
        else
            //スコア倍率表示
            magniText.text = "×" + scoreMagni.ToString("F1");
	}

    /// <summary>
    /// スコア返す
    /// </summary>
    /// <returns></returns>
    public static float ReturnScore()
    {
        return score;
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="addScore"></param>
    public static void ScoreAdd(float addScore)
    {
        //スコア倍率を掛けたものを加算
        score += addScore * scoreMagni;
    }

    /// <summary>
    /// スコア倍率返す
    /// </summary>
    /// <returns></returns>
    public static float ReturnMagnification()
    {
        return scoreMagni;
    }
}
