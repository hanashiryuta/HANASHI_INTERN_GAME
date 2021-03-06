﻿///
///製作日：2018/08/28
///作成者：葉梨竜太
///壁管理クラス
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WallController : NetworkBehaviour {

    //壁の位置
    public GameObject[] wallPoint;
    //爆弾リスト
    [HideInInspector]
    public List<List<GameObject>> bombs;
    //リセットできるかどうか
    bool isReset = false;

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

    void Update()
    {
        //フェードクラスが終了状態なら
        if (FadeController.isSceneEnd)
        {
            //爆弾リセット
            CmdBombsReset();
        }
    }

    //[Command]
    /// <summary>
    /// 爆弾追加
    /// </summary>
    /// <param name="bomb"></param>
    /// <param name="wallType"></param>
    public void CmdAddBombs(GameObject bomb,WallType wallType)
    {
        //自身が投げられた壁のリストに追加
        bombs[(int)wallType].Add(bomb);
            
    }

    //[Command]
    /// <summary>
    /// 爆弾除外
    /// </summary>
    /// <param name="bomb"></param>
    /// <param name="wallType"></param>
    public void CmdRemoveBombs(GameObject bomb, WallType wallType)
    {
        //リストから除外
        bombs[(int)wallType].Remove(bomb);
    }

   // [Command]
    /// <summary>
    /// 爆弾初期化
    /// </summary>
    public void CmdBombsReset()
    {
        if (!isReset)
        {
            //すべての爆弾を除外
            foreach (var cx in bombs)
            {
                for (int i = 0; i < cx.Count; i++)
                {
                    cx[i].GetComponent<BombMove>().bombState = BombState.DEATH;
                }
            }
        }
        isReset = true;
    }
}
