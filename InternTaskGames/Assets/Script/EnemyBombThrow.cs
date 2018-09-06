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
    //public float angle = 30;
    //爆弾を投げれるかどうか
    //public static bool isThrow;
    //アニメーター
    public Animator anim;
    //投擲時SE
    public AudioClip throwSE;
    //オーディオソース
    AudioSource audioSource;

    CountDownController countDownController;
    FadeController fadeController;
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
        //投げられない状態
        //isThrow = true;
        //クラス取得
        enemyDeath = GetComponent<EnemyDeath>();
        //ターゲット検索
        targetObject = GameObject.FindGameObjectWithTag("MainCamera");
        //オーディオソース取得
        audioSource = GetComponent<AudioSource>();
        fadeController = GameObject.Find("Fade").GetComponent<FadeController>();
    }
    void OfflineInitialize()
    {
        //投げられない状態
        //isThrow = true;
        //クラス取得
        enemyDeath = GetComponent<EnemyDeath>();
        //ターゲット検索
        targetObject = GameObject.FindGameObjectWithTag("MainCamera");
        //オーディオソース取得
        audioSource = GetComponent<AudioSource>();
        countDownController = GameObject.Find("CountDownUI").GetComponent<CountDownController>();
        fadeController = GameObject.Find("Fade").GetComponent<FadeController>();
    }

    [ServerCallback]
    void OnlineUpdate()
    {
        if (!fadeController.isSceneEnd)
        {
            //射出までの時間を減らしていく
            throwTime -= Time.deltaTime;
            //射出までの時間が0になったら（時間が来たら）、かつ
            //エネミーが消滅状態でなければ
            if (throwTime <= 0 && !enemyDeath.isDeath)
            {
                //アニメ再生
                anim.SetTrigger("Throw");
                audioSource.PlayOneShot(throwSE);
                //爆弾生成
                GameObject bomb = Instantiate(originBomb, transform.position, Quaternion.identity);
                //爆弾投擲
                //angle = Random.Range(30, 60);
                //BombThrow(bomb, angle,bomb.GetComponent<BombMove>().Grav.y);
                //壁の種類を伝える
                bomb.GetComponent<BombMove>().wallType = wallType;
                //自身を渡す
                //bomb.GetComponent<BombMove>().enemy = this.gameObject;
                bomb.GetComponent<BombMove>().originObject = this.gameObject;
                bomb.GetComponent<BombMove>().targetObject = targetObject;
                bomb.GetComponent<BombMove>().bombState = BombState.RETURNSET;
                NetworkServer.Spawn(bomb);
                //エネミー消滅状態に
                throwTime = 10;
                //enemyDeath.isDeath = true;
            }
        }
    }
    void OfflineUpdate()
    {
        if (countDownController.countDownState == CountDownState.END&&!fadeController.isSceneEnd)
        {
            //射出までの時間を減らしていく
            throwTime -= Time.deltaTime;
            //射出までの時間が0になったら（時間が来たら）、かつ
            //エネミーが消滅状態でなければ
            if (throwTime <= 0 && !enemyDeath.isDeath)
            {
                //アニメ再生
                anim.SetTrigger("Throw");
                audioSource.PlayOneShot(throwSE);
                //爆弾生成
                GameObject bomb = Instantiate(originBomb, transform.position, Quaternion.identity);
                //NetworkServer.Spawn(bomb);
                //爆弾投擲
                //angle = Random.Range(30, 60);
                //BombThrow(bomb, angle,bomb.GetComponent<BombMove>().Grav.y);
                //壁の種類を伝える
                bomb.GetComponent<BombMove>().wallType = wallType;
                //自身を渡す
                //bomb.GetComponent<BombMove>().enemy = this.gameObject;
                bomb.GetComponent<BombMove>().originObject = this.gameObject;
                bomb.GetComponent<BombMove>().targetObject = targetObject;
                bomb.GetComponent<BombMove>().bombState = BombState.RETURNSET;
                //エネミー消滅状態に
                //throwTime = 10;
                enemyDeath.isDeath = true;
            }
        }
    }
}