using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 壁の種類
/// </summary>
public enum WallType
{
    LEFTWALL,//左の壁
    FRONTWALL,//正面の壁
    RIGHTWALL,//右の壁
}
public class EnemySpawnController : NetworkBehaviour
{
    //スポーンポイント配列
    public GameObject[] spawnPoints;
    //基礎エネミー
    public GameObject originEnemy;
    //スポーン時間
    float spawnTime;
    //現在生成する場所
    int nowSpawnPoint = 0;
    //スポーン時間下限
    public int minTimeRange = 5;
    //スポーン時間上限
    public int maxTimeRange = 8;
    //壁の種類
    public WallType wallType;
    //スポーンできるかどうか
    //public static bool isSpawn;
    CountDownController countDownController;
    FadeController fadeController;

    // Use this for initialization
    void Start () {
        if (IsNetwork.isNetConnect)
            OnlineInitialize();
        else
            OfflineInitialize();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (IsNetwork.isNetConnect)
            OnlineUpdate();
        else
            OfflineUpdate();

    }

    void OnlineInitialize()
    {
        //初期化
        //isSpawn = false;
        //スポーン時間設定
        spawnTime = Random.Range(minTimeRange, maxTimeRange);
        //countDownController = GameObject.Find("CountDownUI").GetComponent<CountDownController>();
    }
    void OfflineInitialize()
    {
        //初期化
        //isSpawn = false;
        //スポーン時間設定
        spawnTime = Random.Range(minTimeRange, maxTimeRange);
        countDownController = GameObject.Find("CountDownUI").GetComponent<CountDownController>();
        fadeController = GameObject.Find("Fade").GetComponent<FadeController>();
    }

    void OnlineUpdate()
    {
        if (fadeController == null)
        {
            fadeController = GameObject.Find("Fade").GetComponent<FadeController>();
            return;
        }
        if (!fadeController.isSceneEnd)
        {
            //スポーン時間を減らす
            spawnTime -= Time.deltaTime;
            //スポーン時間が0以下になったら（時間が来たら）
            if (spawnTime <= 0)
            {
                //スポーンポイント設定
                nowSpawnPoint = Random.Range(0, spawnPoints.Length);
                //そのスポーンポイントにエネミーがいなければ
                if (spawnPoints[nowSpawnPoint].transform.childCount <= 0)
                {
                    //エネミー生成
                    GameObject enemy = Instantiate(originEnemy);
                    enemy.transform.position = spawnPoints[nowSpawnPoint].transform.position;
                    //エネミーに壁の種類を渡す
                    enemy.GetComponent<EnemyBombThrow>().wallType = wallType;
                    NetworkServer.Spawn(enemy);
                    //スポーン時間再設定
                    spawnTime = Random.Range(minTimeRange, maxTimeRange);
                }
            }
        }
    }
    void OfflineUpdate()
    {
        if (countDownController.countDownState == CountDownState.END&&!fadeController.isSceneEnd)
        {
            //スポーン時間を減らす
            spawnTime -= Time.deltaTime;
            //スポーン時間が0以下になったら（時間が来たら）
            if (spawnTime <= 0)
            {
                //スポーンポイント設定
                nowSpawnPoint = Random.Range(0, spawnPoints.Length);
                //そのスポーンポイントにエネミーがいなければ
                if (spawnPoints[nowSpawnPoint].transform.childCount <= 0)
                {
                    //エネミー生成
                    GameObject enemy = Instantiate(originEnemy);
                    enemy.transform.position = spawnPoints[nowSpawnPoint].transform.position;
                    enemy.transform.parent = spawnPoints[nowSpawnPoint].transform;
                    //エネミーに壁の種類を渡す
                    enemy.GetComponent<EnemyBombThrow>().wallType = wallType;
                    //NetworkServer.Spawn(enemy);
                    //スポーン時間再設定
                    spawnTime = Random.Range(minTimeRange, maxTimeRange);
                }
            }
        }
    }
}
