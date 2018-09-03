///
///製作日：2018/09/03
///作成者：葉梨竜太
///BGM管理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour {

    //インスタンス
    static BGMManager instance;

    //タイトルでのBGM
    public AudioClip titleBGM;
    //ゲームメインでのBGM
    public AudioClip gameMainBGM;
    //リザルトでのBGM
    public AudioClip resultBGM;
    //オーディオソース
    AudioSource audioSource;

	// Use this for initialization
	void Awake () {
        //シングルトン処理
		if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //オーディオソース取得
        audioSource = gameObject.GetComponent<AudioSource>();
        //シーン読み込み時処理
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// シーン読み込み時処理
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //BGMストップ
        audioSource.Stop();
        //読み込んだシーンによってBGM切り替え
        switch(scene.name)
        {
            //タイトル
            case "Title":
                audioSource.clip = titleBGM;
                break;
            //ゲームメイン
            case "gameMain":
                audioSource.clip = gameMainBGM;
                break;
          　//リザルト
            case "Result":
                audioSource.clip = resultBGM;
                break;
        }
        //BGMスタート
        audioSource.Play();
    }
}
