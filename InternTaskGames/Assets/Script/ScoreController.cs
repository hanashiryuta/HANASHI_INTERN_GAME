///
///製作日：2018/08/29
///作成者：葉梨竜太
///スコア管理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
    //カウントダウンクラス
    CountDownController countDownController;
    //ハイスコアが保存できるかどうか
    bool isHighScoreSet;

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
        //ランキングがなければ初期化
        if (!PlayerPrefs.HasKey("Ranking"))
        {
            PlayerPrefs.SetString("Ranking","0,0,0,0,0,0,0,0,0,0");
        }
        //カウントダウンクラス取得
        countDownController = GameObject.Find("CountDownUI").GetComponent<CountDownController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //カウントダウンが終わっていたら
        if (countDownController.countDownState == CountDownState.END)
        {
            //表示
            scoreText.enabled = true;
            //表示
            magniText.enabled = true;
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
        else
        {
            //非表示
            scoreText.enabled = false;
            //非表示
            magniText.enabled = false;
        }
        //フェードが終了状態なら
        if(FadeController.isSceneEnd)
        {
            //ハイスコアセーブ
            SavedHighScore();
        }
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

    /// <summary>
    /// ハイスコア保存クラス
    /// </summary>
    public void SavedHighScore()
    {
        if (!isHighScoreSet)
        {
            //ランキング取得
            string Ranking = PlayerPrefs.GetString("Ranking");
            //配列作成
            string[] rankString = Ranking.Split(',');
            //floatリスト作成
            List<float> ranking = new List<float>();

            //数値化ランキング格納
            foreach (var cx in rankString)
            {
                try
                {
                    ranking.Add(float.Parse(cx));
                }
                catch (FormatException)
                {
                    break;
                }
            }

            //現在のスコア保存
            float charenger = score;
            //ランキングと順次比較
            for (int i = 0; i < ranking.Count; i++)
            {
                //リストからひとつ取り出す
                float ranker = ranking[i];
                //取り出したものより保存しているものが大きければ
                if (ranker < charenger)
                {
                    //ランキング更新
                    float change = ranker;
                    ranker = charenger;
                    charenger = change;
                }
                //リストに戻す
                ranking[i] = ranker;
            }

            //保存用文字列
            string setRanking = "";
            //ランキング文字列化
            for (int i = 0; i < ranking.Count - 1; i++)
            {
                setRanking += ranking[i].ToString() + ",";
            }
            //最後は’、’を含まない
            setRanking += ranking[ranking.Count - 1].ToString();
            //ランキング更新
            PlayerPrefs.DeleteKey("Ranking");
            PlayerPrefs.SetString("Ranking", setRanking);
        }
        isHighScoreSet = true;
    }
}
