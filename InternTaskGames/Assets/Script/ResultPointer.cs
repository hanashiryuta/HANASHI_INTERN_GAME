///
///製作日：2018/08/29
///作成者：葉梨竜太
///リザルト画面でのボタン処理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPointer : MonoBehaviour {
    
    /// <summary>
    /// タイトルに戻る
    /// </summary>
    public void TitleBackButton()
    {
        GameObject.Find("Fade").GetComponent<FadeController>().isSceneEnd = true;
    }

    /// <summary>
    /// ゲームが終了する
    /// </summary>
    public void GameEndButton()
    {

    }
}
