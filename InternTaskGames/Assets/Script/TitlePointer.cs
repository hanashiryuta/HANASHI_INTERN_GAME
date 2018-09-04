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

    //クリックSE
    public AudioClip clickSE;
    //オーディオソース
    AudioSource audioSource;

    void Start()
    {
        //オーディオソース取得
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// ゲーム開始
    /// </summary>
    public void GameStartButton()
    {
        //クリックSE再生
        audioSource.PlayOneShot(clickSE);
        //フェードアウト開始
        GameObject.Find("Fade").GetComponent<FadeController>().isSceneEnd = true;
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void GameEndButton()
    {
        //クリックSE再生
        audioSource.PlayOneShot(clickSE);
        //アプリケーション終了
        Application.Quit();
    }

    public void MultiLobbyButton()
    {
        //クリックSE再生
        audioSource.PlayOneShot(clickSE);
        GameObject.Find("Fade").GetComponent<FadeController>().nextScene = "Lobby";
        //フェードアウト開始
        GameObject.Find("Fade").GetComponent<FadeController>().isSceneEnd = true;
    }
}
