///
///製作日：2018/09/06
///作成者：葉梨竜太
///ラケット移動範囲クラス
///
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
    //コントローラ
    public GameObject controller;
    //移動半径
    float racketRange = 10;
    //移動半径用壁
    public GameObject[] racketRangeWalls;
    //1フレーム前のタッチパネルスライド位置
    float currentRacketRange;
    //現在のタッチパネルスライド位置
    float previousRacketRange;
    //消すオブジェクト
    public string[] falseObjectName;

    // Use this for initialization
    void Start()
    {
        //自身がローカルプレイヤーで、オンライン状態なら
        if (!isLocalPlayer&&IsNetwork.isOnline)
        {
            //消すオブジェクトすべて
            foreach (var cx in falseObjectName)
            {
                //非アクティブ状態へ
                transform.Find(cx).gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //オンラインかオフラインかで動作変更
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
        //自身がローカルなプレイヤーだったら
        if (isLocalPlayer)
        {
            //ラケット移動処理
            RacketMoved();
            //サーバーへ位置を送信
            CmdMoveRacket(racket.transform.position, racket.transform.rotation);            
        }
    }

    /// <summary>
    /// オフライン時のアップデート
    /// </summary>
    void OfflineUpdate()
    {
        //ラケット移動処理
        RacketMoved();
    }

    /// <summary>
    /// ラケット移動処理
    /// </summary>
    void RacketMoved()
    {
        //移動制限距離設定
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

    /// <summary>
    /// サーバーへ位置送信
    /// </summary>
    /// <param name="racketPosition"></param>
    /// <param name="racketRotation"></param>
    [Command]
    void CmdMoveRacket(Vector3 racketPosition, Quaternion racketRotation)
    {
        //サーバーに接続しているクライアントからすべて
        foreach (var conn in NetworkServer.connections)
        {
            //つながっていないやつは除外
            if (conn == null || !conn.isReady)
                continue;
            //自身も除外
            if (conn == connectionToClient)
                continue;

            //サーバーから位置送信
            TargetSyncTransform(conn, racketPosition, racketRotation);
        }
    }

    /// <summary>
    /// サーバーから位置送信
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    [TargetRpc]
    void TargetSyncTransform(NetworkConnection conn, Vector3 position, Quaternion rotation)
    {
        //ラケット位置更新
        racket.transform.SetPositionAndRotation(position, rotation);
    }

    /// <summary>
    /// ラケット移動半径設定
    /// </summary>
    void RacketRangeSet()
    {
        //コントローラのタッチパネルをスライドさせた位置を保存
        previousRacketRange = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad).y;

        //上にスライドしていたら
        if (previousRacketRange > currentRacketRange)
        {
            //20以上離れない
            if (racketRange < 20)
                //移動制限増加
                racketRange++;
        }
        //下にスライドさせていたら
        else if (previousRacketRange < currentRacketRange)
        {
            //5以上近づかない
            if (racketRange > 5)
                //移動制限縮小
                racketRange--;
        }

        //距離によって制限距離用の壁の大きさ、位置を設定
        foreach (var cx in racketRangeWalls)
        {
            //位置設定
            cx.transform.localPosition = new Vector3(cx.transform.localPosition.x, cx.transform.localPosition.y, racketRange);
            //大きさ設定
            cx.transform.localScale = new Vector3(racketRange, cx.transform.localScale.y, cx.transform.localScale.z);
        }
        //スライド位置更新
        currentRacketRange = previousRacketRange;
    }
}
