using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyBombThrow : NetworkBehaviour
{
    //壁の種類
    [HideInInspector]
    public WallType wallType = WallType.FRONTWALL;

    //基礎爆弾
    public GameObject originBomb;
    //投擲までの時間
    float throwTime = 2.0f;
    //エネミーの消滅処理クラス
    EnemyDeath enemyDeath;
    //ターゲットオブジェクト
    GameObject targetObject;
    //アニメーター
    public Animator anim;
    //投擲時SE
    public AudioClip throwSE;
    //オーディオソース
    AudioSource audioSource;
    //カウントダウンクラス
    CountDownController countDownController;

    // Use this for initialization
    void Start()
    {
        //クラス取得
        enemyDeath = GetComponent<EnemyDeath>();
        //ターゲット検索
        targetObject = GameObject.FindGameObjectWithTag("MainCamera");
        //オーディオソース取得
        audioSource = GetComponent<AudioSource>();

        //オフラインなら
        if (!IsNetwork.isOnline)
            //カウントダウンクラス取得
            countDownController = GameObject.Find("CountDownUI").GetComponent<CountDownController>();
    }

    // Update is called once per frame
    void Update()
    {
        //オンラインとオフラインとで動作を変える
        if (IsNetwork.isOnline)
            OnlineUpdate();
        else
            OfflineUpdate();

    }

    /// <summary>
    /// オンライン時のアップデート
    /// </summary>
    [ServerCallback]
    void OnlineUpdate()
    {
        //フェードが終了状態でなければ
        if (!FadeController.isSceneEnd)
        {
            //射出までの時間を減らしていく
            throwTime -= Time.deltaTime;
            //射出までの時間が0になったら（時間が来たら）、かつ
            //エネミーが消滅状態でなければ
            if (throwTime <= 0 && !enemyDeath.isDeath)
            {                
                //サーバーでエネミースポーン
                NetworkServer.Spawn(BombThrow());
            }
        }
    }

    /// <summary>
    /// オフライン時のアップデート
    /// </summary>
    void OfflineUpdate()
    {
        //カウントダウンが終了しており、フェードがシーン終了状態でなければ
        if (countDownController.countDownState == CountDownState.END&&!FadeController.isSceneEnd)
        {
            //射出までの時間を減らしていく
            throwTime -= Time.deltaTime;
            //射出までの時間が0になったら（時間が来たら）、かつ
            //エネミーが消滅状態でなければ
            if (throwTime <= 0 && !enemyDeath.isDeath)
            {
                //爆弾投擲
                BombThrow();
                //死亡可能状態へ
                enemyDeath.isDeath = true;
            }
        }
    }

    /// <summary>
    /// 爆弾投擲
    /// </summary>
    /// <returns></returns>
    GameObject BombThrow()
    {         
        //アニメ再生
        anim.SetTrigger("Throw");
        //SE再生
        audioSource.PlayOneShot(throwSE);
        //爆弾生成
        GameObject bomb = Instantiate(originBomb, transform.position, Quaternion.identity);
        //壁の種類を伝える
        bomb.GetComponent<BombMove>().wallType = wallType;
        //自身を渡す
        bomb.GetComponent<BombMove>().originObject = this.gameObject;
        //ターゲットを渡す
        bomb.GetComponent<BombMove>().targetObject = targetObject;
        //爆弾の状態を遷移
        bomb.GetComponent<BombMove>().bombState = BombState.THROWSET;
        //エネミー消滅状態に
        throwTime = 10;
        return bomb;
    }
}