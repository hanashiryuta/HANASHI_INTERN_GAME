///
///製作日：2018/08/30
///作成者：葉梨竜太
///フェード管理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// フェード状態
/// </summary>
public enum FadeActionState
{
    SceneStart,//シーン開始時
    Stay,//待機
    SceneEnd,//シーン終了時
}

public class FadeController : MonoBehaviour {
    
    //フェード画像
    public Image fadeImage;
    //保存用カラー
    Color fadeColor;
    //α値
    float alpha = 1.3f;
    //次のシーン
    public string nextScene;
    [HideInInspector]
    public bool isSceneStart;
    //シーンが終了するかどうか
    [HideInInspector]
    public bool isSceneEnd;
    //フェード状態
    [HideInInspector]
    public FadeActionState fadeActionState = FadeActionState.SceneStart;

	// Use this for initialization
	void Start () {
        //フェード状態初期化
        fadeActionState = FadeActionState.SceneStart;
        //カラー保存
        fadeColor = fadeImage.color;
        //α値初期化
        fadeColor.a = alpha;
        //カラー帰還
        fadeImage.color = fadeColor;
	}
	
	// Update is called once per frame
	void Update () {
        //フェード状態によって処理を変更
        switch(fadeActionState)
        {
            //シーン開始時
            case FadeActionState.SceneStart:
                //αを減らしていく
                alpha -= Time.deltaTime / 2;
                //透明になったら
                if (alpha <= 0.0f)
                {
                    isSceneStart = true;
                    //状態遷移
                    fadeActionState = FadeActionState.Stay;
                }
                break;
            //待機状態
            case FadeActionState.Stay:
                //シーン終了可になったら
                if (isSceneEnd)
                    //状態遷移
                    fadeActionState = FadeActionState.SceneEnd;
                break;
            //シーン終了時
            case FadeActionState.SceneEnd:
                //αを増やしていく
                alpha += Time.deltaTime / 2;
                //イメージが表示されたら
                if (alpha >= 1.3f)
                {
                    //シーン移行
                    SceneManager.LoadScene(nextScene);
                }
                break;
        }

        //α値更新
        fadeColor.a = alpha;
        //カラー帰還
        fadeImage.color = fadeColor;            
	}
}
