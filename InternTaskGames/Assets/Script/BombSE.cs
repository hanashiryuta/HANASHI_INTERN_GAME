///
///製作日：2018/09/03
///作成者：葉梨竜太
///爆発時SE再生オブジェクト
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSE : MonoBehaviour {

    //爆発時SE
    public AudioClip explosionSE;
    //オーディオソース
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        //オーディオソース取得
        audioSource = GetComponent<AudioSource>();
        //SE鳴らす
        audioSource.PlayOneShot(explosionSE);
	}
	
	// Update is called once per frame
	void Update () {
        //SEが鳴り終わったら
        if (!audioSource.isPlaying)
            //オブジェクト消滅
            Destroy(gameObject);
	}
}
