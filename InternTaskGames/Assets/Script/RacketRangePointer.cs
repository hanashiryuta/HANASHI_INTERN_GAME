using System;
///
///製作日：2018/08/28
///作成者：葉梨竜太
///ラケットの位置ポイントクラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RacketRangePointer : NetworkBehaviour {

    //ラケット
    public GameObject racket;
    //レイヤー
    public LayerMask layerMask;
    public GameObject controller;
    
    float racketRange = 10;
    public GameObject[] racketRangeWalls;

    float currentRacketRange;
    float previousRacketRange;

	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            CmdRacketRangeSet();
            //コントローラからレイを飛ばす
            Ray pointer = new Ray(controller.transform.position, controller.transform.forward);

            RaycastHit hit;
            //レイが指定したレイヤーを持つオブジェクトに当たったら
            if (Physics.Raycast(pointer, out hit, 100, layerMask))
            {
                CmdMoveRacket(hit.point);
            }
        }
	}

    [Command]
    void CmdMoveRacket(Vector3 position)
    {
        //その位置にラケット配置
        racket.transform.position = position;
        //当たった位置との角度計算
        float angle = Mathf.Atan2(racket.transform.position.z - controller.transform.position.z, racket.transform.position.x - controller.transform.position.x) * 180 / Mathf.PI;
        //ラケットを回転
        racket.transform.rotation = Quaternion.Euler(0, -angle + 90, 0);
        foreach(var conn in NetworkServer.connections)
        {
            if (conn == null || !conn.isReady)
                continue;
            if (conn == connectionToClient)
                continue;

            TargetSyncTransform(conn, racket.transform.position, racket.transform.rotation);
        }

    }

    [TargetRpc]
    void TargetSyncTransform(NetworkConnection conn, Vector3 position, Quaternion rotation)
    {
        racket.transform.SetPositionAndRotation(position, rotation);
    }

    [Command]
    void CmdRacketRangeSet()
    {
        previousRacketRange = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad).y;
        if (previousRacketRange > currentRacketRange)
        {
            if (racketRange < 20)
                racketRange++;
        }
        else if (previousRacketRange < currentRacketRange)
        {
            if (racketRange > 5)
                racketRange--;

            foreach (var cx in racketRangeWalls)
            {
                cx.transform.localPosition = new Vector3(cx.transform.localPosition.x, cx.transform.localPosition.y, racketRange);
                cx.transform.localScale = new Vector3(racketRange, cx.transform.localScale.y, cx.transform.localScale.z);
            }

        }
        currentRacketRange = previousRacketRange;
    }

}
