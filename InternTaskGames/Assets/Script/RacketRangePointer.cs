///
///製作日：2018/08/28
///作成者：葉梨竜太
///ラケットの位置ポイントクラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketRangePointer : MonoBehaviour {

    //ラケット
    public GameObject racket;
    //レイヤー
    public LayerMask layerMask;

    float racketRange = 10;
    GameObject[] racketRangeWalls;
    [HideInInspector]
    bool isMulti;

    float currentRacketRange;
    float previousRacketRange;

	// Use this for initialization
	void Start () {
        racketRangeWalls = GameObject.FindGameObjectsWithTag("RacketMoveRange");
	}
	
	// Update is called once per frame
	void Update () {
        //コントローラからレイを飛ばす
        Ray pointer = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        //レイが指定したレイヤーを持つオブジェクトに当たったら
        if (Physics.Raycast(pointer, out hit, 100, layerMask))
        {
            //その位置にラケット配置
            racket.transform.position = hit.point;
        }

        //当たった位置との角度計算
        float angle = Mathf.Atan2(racket.transform.position.z - transform.position.z, racket.transform.position.x - transform.position.x) * 180/Mathf.PI;

        //ラケットを回転
        racket.transform.rotation = Quaternion.Euler(0, -angle + 90, 0);

        RacketRangeSet();

        foreach(var cx in racketRangeWalls)
        {
            cx.transform.localPosition = new Vector3(cx.transform.localPosition.x, cx.transform.localPosition.y, racketRange);

            if(!isMulti)
            cx.transform.localScale = new Vector3(racketRange, cx.transform.localScale.y, cx.transform.localScale.z);
        }

        currentRacketRange = previousRacketRange;
	}

    void RacketRangeSet()
    {
        previousRacketRange = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad).y;
        if(!isMulti)
        {
            if (previousRacketRange > currentRacketRange)
            {
                if (racketRange < 20)
                    racketRange++;
            }
            else if (previousRacketRange < currentRacketRange)
            {
                if (racketRange > 5)
                    racketRange--;
            }
        }
        else
        {
            if (previousRacketRange > currentRacketRange)
            {
                if (racketRange < 1)
                    racketRange--;
            }
            else if (previousRacketRange < currentRacketRange)
            {
                if (racketRange > 27)
                    racketRange++;
            }
        }
    }
}
