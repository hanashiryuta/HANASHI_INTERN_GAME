using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RacketRangePointer : NetworkBehaviour
{
    //ラケット
    public GameObject racket;
    //レイヤー
    public LayerMask layerMask;
    public GameObject controller;

    float racketRange = 10;
    public GameObject[] racketRangeWalls;

    float currentRacketRange;
    float previousRacketRange;

    public string[] falseObjectName;
    // Use this for initialization
    void Start()
    {
        if (IsNetwork.isNetConnect)
            OnlineInitialize();
        else
            OfflineInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsNetwork.isNetConnect)
            OnlineUpdate();
        else
            OfflineUpdate();

    }

    void OnlineInitialize()
    {
        if (!isLocalPlayer)
        {
            foreach (var cx in falseObjectName)
            {
                transform.Find(cx).gameObject.SetActive(false);
            }
        }
    }
    void OfflineInitialize()
    {

    }

    void OnlineUpdate()
    {
        if (isLocalPlayer)
        {
            RacketRangeSet();
            //コントローラからレイを飛ばす
            Ray pointer = new Ray(controller.transform.position, controller.transform.forward);

            RaycastHit hit;
            //レイが指定したレイヤーを持つオブジェクトに当たったら
            if (Physics.Raycast(pointer, out hit, 100, layerMask))
            {
                //その位置にラケット配置
                racket.transform.position = hit.point;
                //当たった位置との角度計算
                float angle = Mathf.Atan2(racket.transform.position.z - controller.transform.position.z, racket.transform.position.x - controller.transform.position.x) * 180 / Mathf.PI;
                //ラケットを回転
                racket.transform.rotation = Quaternion.Euler(0, -angle + 90, 0);
                CmdMoveRacket(racket.transform.position, racket.transform.rotation);
            }
        }
    }
    void OfflineUpdate()
    {
        RacketRangeSet();
        //コントローラからレイを飛ばす
        Ray pointer = new Ray(controller.transform.position, controller.transform.forward);

        RaycastHit hit;
        //レイが指定したレイヤーを持つオブジェクトに当たったら
        if (Physics.Raycast(pointer, out hit, 100, layerMask))
        {
            //その位置にラケット配置
            racket.transform.position = hit.point;
            //当たった位置との角度計算
            float angle = Mathf.Atan2(racket.transform.position.z - controller.transform.position.z, racket.transform.position.x - controller.transform.position.x) * 180 / Mathf.PI;
            //ラケットを回転
            racket.transform.rotation = Quaternion.Euler(0, -angle + 90, 0);
        }
    }

    [Command]
    void CmdMoveRacket(Vector3 racketPosition, Quaternion racketRotation)
    {
        foreach (var conn in NetworkServer.connections)
        {
            if (conn == null || !conn.isReady)
                continue;
            if (conn == connectionToClient)
                continue;

            TargetSyncTransform(conn, racketPosition, racketRotation);
        }
    }

    [TargetRpc]
    void TargetSyncTransform(NetworkConnection conn, Vector3 position, Quaternion rotation)
    {
        racket.transform.SetPositionAndRotation(position, rotation);
    }

    void RacketRangeSet()
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
        }
        foreach (var cx in racketRangeWalls)
        {
            cx.transform.localPosition = new Vector3(cx.transform.localPosition.x, cx.transform.localPosition.y, racketRange);
            cx.transform.localScale = new Vector3(racketRange, cx.transform.localScale.y, cx.transform.localScale.z);
        }
        currentRacketRange = previousRacketRange;
    }
}
