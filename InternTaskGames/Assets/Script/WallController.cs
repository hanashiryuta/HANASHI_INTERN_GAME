///
///製作日：2018/08/28
///作成者：葉梨竜太
///壁管理クラス
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

    //壁の位置
    public GameObject[] wallPoint;
    //爆弾リスト
    [HideInInspector]
    public List<List<GameObject>> bombs;

    void Start()
    {
        //爆弾リスト生成
        bombs = new List<List<GameObject>>()
        {
            new List<GameObject>(),
            new List<GameObject>(),
            new List<GameObject>()
        };
    }

    /// <summary>
    /// 爆弾追加
    /// </summary>
    /// <param name="bomb"></param>
    /// <param name="wallType"></param>
    public void AddBombs(GameObject bomb,WallType wallType)
    {
        //自身が投げられた壁のリストに追加
        bombs[(int)wallType].Add(bomb);
    }

    /// <summary>
    /// 爆弾除外
    /// </summary>
    /// <param name="bomb"></param>
    /// <param name="wallType"></param>
    public void RemoveBombs(GameObject bomb, WallType wallType)
    {
        //リストから除外
        bombs[(int)wallType].Remove(bomb);
    }

    /// <summary>
    /// 爆弾初期化
    /// </summary>
    public void BombsReset()
    {
        //すべての爆弾を除外
        foreach(var cx in bombs)
        {
            for(int i = 0; i < cx.Count; i++)
            {
                cx[i].GetComponent<BombMove>().bombState = BombState.DEATH;
            }
        }
    }
}
