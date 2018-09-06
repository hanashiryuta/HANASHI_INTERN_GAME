///
///製作日：2018/09/05
///作成者：葉梨竜太
///改変Ip取得クラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkDiscover : NetworkDiscovery {

    /// <summary>
    /// アドレスを受け取った時の処理
    /// </summary>
    /// <param name="fromAddress"></param>
    /// <param name="data"></param>
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        //ネットワークマネージャー取得
        NetworkManager manager = NetworkManager.singleton;
        //すでにつながっているなら何もしない
        if (manager.isNetworkActive)
            return;
        //アドレス取得
        manager.networkAddress = fromAddress;
        //クライアント起動
        manager.StartClient();
        //同じメソッドで呼ぶとエラーが起きるため次フレーム
        StartCoroutine(StopBroadcastCoroutine());
    }

    /// <summary>
    /// ブロードキャスト取得停止
    /// </summary>
    /// <returns></returns>
    IEnumerator StopBroadcastCoroutine()
    {
        //1フレーム後
        yield return new WaitForEndOfFrame();
        //ブロードキャスト停止
        StopBroadcast();
    }
}
