///
///製作日：2018/09/06
///作成者：葉梨竜太
///エネミー生成処理クラス
///
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
    //カウントダウンクラス
    CountDownController countDownController;

    // Use this for initialization
    void Start ()
    {
        //スポーン時間設定
        spawnTime = Random.Range(minTimeRange, maxTimeRange);

        //オフラインなら
        if (!IsNetwork.isOnline)
        {
            //カウントダウンクラス取得
            countDownController = GameObject.Find("CountDownUI").GetComponent<CountDownController>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //オンラインかオフラインかで動作を変える
        if (IsNetwork.isOnline)
            OnlineUpdate();
        else
            OfflineUpdate();

    }
    
    /// <summary>
    /// オンライン時のアップデート
    /// </summary>
    void OnlineUpdate()
    {
        //フェードが終了状態でなければ
        if (!FadeController.isSceneEnd)
        {
            //スポーン時間を減らす
            spawnTime -= Time.deltaTime;
            //生成できるなら
            if (isSpawnCheck())
                //サーバーで生成
                NetworkServer.Spawn(EnemySpawn());
        }
    }

    /// <summary>
    /// オフライン時のアップデート
    /// </summary>
    void OfflineUpdate()
    {
        //カウントダウンが終了し、フェードが終了状態でなければ
        if (countDownController.countDownState == CountDownState.END && !FadeController.isSceneEnd)
        {
            //スポーン時間を減らす
            spawnTime -= Time.deltaTime;
            //生成できるなら
            if (isSpawnCheck())
                //エネミー生成
                EnemySpawn();
        }
    }    

    /// <summary>
    /// 生成できるかどうか
    /// </summary>
    /// <returns></returns>
    bool isSpawnCheck()
    { 
        //スポーン時間が0以下になったら（時間が来たら）
        if (spawnTime <= 0)
        {
            //スポーンポイント設定
            nowSpawnPoint = Random.Range(0, spawnPoints.Length);
            //そのスポーンポイントにエネミーがいなければ
            if (spawnPoints[nowSpawnPoint].transform.childCount <= 0)
            {
                //生成できる
                return true;
            }
        }
        //生成できない
        return false;
    }

    /// <summary>
    /// 敵出現処理
    /// </summary>
    /// <returns></returns>
    GameObject EnemySpawn()
    {
        //エネミー生成
        GameObject enemy = Instantiate(originEnemy);
        //位置設定
        enemy.transform.position = spawnPoints[nowSpawnPoint].transform.position;
        //親オブジェクト指定
        enemy.transform.parent = spawnPoints[nowSpawnPoint].transform;
        //エネミーに壁の種類を渡す
        enemy.GetComponent<EnemyBombThrow>().wallType = wallType;
        //スポーン時間再設定
        spawnTime = Random.Range(minTimeRange, maxTimeRange);
        return enemy;
    }
}
