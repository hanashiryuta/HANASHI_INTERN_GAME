///
///製作日：2018/08/29
///作成者：葉梨竜太
///リザルト画面でのボタン処理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPointer : MonoBehaviour
{
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
    /// タイトルに戻る
    /// </summary>
    public void TitleBackButton()
    {
        //クリックSE再生
        audioSource.PlayOneShot(clickSE);
        //フェードアウト開始
        GameObject.Find("Fade").GetComponent<FadeController>().isSceneEnd = true;
    }

    /// <summary>
    /// ゲームが終了する
    /// </summary>
    public void GameEndButton()
    {
        //クリックSE再生
        audioSource.PlayOneShot(clickSE);
        //アプリケーション終了
        Application.Quit();
    }
}
