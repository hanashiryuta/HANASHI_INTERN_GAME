///
///製作日：2018/08/29
///作成者：葉梨竜太
///タイトル画面でのボタン処理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitlePointer : MonoBehaviour {

    /// <summary>
    /// ゲーム開始
    /// </summary>
    public void GameStartButton()
    {
        GameObject.Find("Fade").GetComponent<FadeController>().isSceneEnd = true;
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void GameEndButton()
    {

    }
}
