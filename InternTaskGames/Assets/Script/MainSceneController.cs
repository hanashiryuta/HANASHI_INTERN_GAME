///
///製作日：2018/08/31
///作成者：葉梨竜太
///メインシーン管理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour {

    //フェード管理クラス
    FadeController fadeController;
    //シーンが始まるか
    bool isSceneStart = false;
    //シーンが終わるか
    bool isSceneEnd = false;

    void Start()
    {
        //フェードコントローラー取得
        fadeController = GameObject.Find("Fade").GetComponent<FadeController>();
    }

    void Update()
    {
        //フェード管理クラスが待機状態だったら
        if(fadeController.fadeActionState == FadeActionState.Stay)
        {
            //シーンを始める
            SceneStart();
        }
        //フェード管理クラスがシーン終了時状態だったら
        else if(fadeController.fadeActionState == FadeActionState.SceneEnd)
        {
            //シーンを終わる
            SceneEnd();
        }
    }

    /// <summary>
    /// シーン開始処理
    /// </summary>
    public void SceneStart()
    {
        if (!isSceneStart)
        {
            //時間開始
            TimeController.isTimeStart = true;
            //エネミースポーン開始
            EnemySpawnController.isSpawn = true;
            //エネミーボム投擲開始
            EnemyBombThrow.isThrow = true;
            //フラグ切り替え
            isSceneStart = true;
        }
    }
    /// <summary>
    /// シーン終了処理
    /// </summary>
    public void SceneEnd()
    {
        if (!isSceneEnd)
        {
            //時間停止
            TimeController.isTimeStart = false;
            //スポーン停止
            EnemySpawnController.isSpawn = false;
            //投擲停止
            EnemyBombThrow.isThrow = false;
            //爆弾リセット
            GameObject.Find("Walls").GetComponent<WallController>().BombsReset();
            //ハイスコア処理
            ScoreController.SavedHighScore();
            //フラグ切り替え
            isSceneEnd = true;
        }
    }
}
