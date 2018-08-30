///
///製作日：2018/08/29
///作成者：葉梨竜太
///リザルトシーンでのスコア管理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class ResltScoreController : MonoBehaviour {

    //リザルト表示オブジェクト
    public GameObject resultTextObj;
    //リザルト表示テキスト
    Text resultText;
    //スコア
    float score;

    public GameObject resultRankingObj;
    Text resultRankingText;

	// Use this for initialization
	void Start () {
        //スコア取得
        score = ScoreController.ReturnScore();
        //リザルト表示テキスト取得
        resultText = resultTextObj.GetComponent<Text>();
        //リザルトスコア表示
        resultText.text = "Score:" + score.ToString();
        resultRankingText = resultRankingObj.GetComponent<Text>();
        string Ranking = PlayerPrefs.GetString("Ranking");
        string[] rankString = Ranking.Split(',');
        string rank = "";
        foreach(var cx in rankString)
        {
            rank += cx + "\n";
        }
        resultRankingText.text = rank;
    }
}
