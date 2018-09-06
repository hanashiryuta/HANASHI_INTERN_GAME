using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsNetwork : MonoBehaviour {

    static IsNetwork instance;

    public static bool isNetConnect = false;

	// Use this for initialization
	void Awake () {
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
        //読み込んだシーンによってBGM切り替え
        switch (scene.name)
        {
            //タイトル
            case "Title":
                isNetConnect = false;
                break;
            //ゲームメイン
            case "gameMain":
                isNetConnect = false;
                break;
            //リザルト
            case "Result":
                isNetConnect = false;
                break;
            case "Lobby":
                isNetConnect = true;
                break;
            case "VSMode":
                isNetConnect = true;
                break;
        }
    }
}
