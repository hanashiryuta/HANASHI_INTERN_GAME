///
///製作日：2018/08/28
///作成者：葉梨竜太
///壁管理クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

    //壁の位置
    public GameObject[] wallPoint;

    [HideInInspector]
    public List<List<GameObject>> bombs;

    void Start()
    {
        bombs = new List<List<GameObject>>()
        {
            new List<GameObject>(),
            new List<GameObject>(),
            new List<GameObject>()
        };
    }

    public void AddBombs(GameObject bomb,WallType wallType)
    {
        bombs[(int)wallType].Add(bomb);
    }

    public void RemoveBombs(GameObject bomb, WallType wallType)
    {
        bombs[(int)wallType].Remove(bomb);
    }
    
}
