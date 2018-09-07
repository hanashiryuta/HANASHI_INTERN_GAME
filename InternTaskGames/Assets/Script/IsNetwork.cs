///
///製作日：2018/09/06
///作成者：葉梨竜太
///ネットワーク判定クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsNetwork : MonoBehaviour {

    //インスタンス
    static IsNetwork instance;
    //オンラインかどうか
    public static bool isOnline = true;

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
        //読み込んだシーンによってフラグ切り替え
        switch (scene.name)
        {
            //タイトル
            case "Title":
                //オフライン
                isOnline = false;
                break;
            //ゲームメイン
            case "gameMain":
                //オフライン
                isOnline = false;
                break;
            //リザルト
            case "Result":
                //オフライン
                isOnline = false;
                break;
            case "Lobby":
                //オンライン
                isOnline = true;
                break;
            case "VSMode":
                //オンライン
                isOnline = true;
                break;
        }
    }
}
