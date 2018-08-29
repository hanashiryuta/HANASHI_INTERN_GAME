///
///製作日：2018/08/27
///作成者：葉梨竜太
///エネミーの出現管理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 壁の種類
/// </summary>
public enum WallType
{
    LEFTWALL,//左の壁
    FRONTWALL,//正面の壁
    RIGHTWALL,//右の壁
}

public class EnemySpawnController : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
        //スポーン時間設定
        spawnTime = Random.Range(minTimeRange, maxTimeRange);
	}
	
	// Update is called once per frame
	void Update () {
        //スポーン時間を減らす
        spawnTime -= Time.deltaTime;
        //スポーン時間が0以下になったら（時間が来たら）
        if(spawnTime <= 0)
        {
            //スポーンポイント設定
            nowSpawnPoint = Random.Range(0, spawnPoints.Length);
            //そのスポーンポイントにエネミーがいなければ
            if (spawnPoints[nowSpawnPoint].transform.childCount <= 0)
            { 
                //エネミー生成
                GameObject enemy = Instantiate(originEnemy, spawnPoints[nowSpawnPoint].transform.position, Quaternion.identity, spawnPoints[nowSpawnPoint].transform);
                //エネミーに壁の種類を渡す
                enemy.GetComponent<EnemyBombThrow>().wallType = wallType;
                //スポーン時間再設定
                spawnTime = Random.Range(minTimeRange, maxTimeRange);
            }
        }
	}
}
