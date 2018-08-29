///
///製作日：2018/08/28
///作成者：葉梨竜太
///ラケットの動きクラス
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ラケットの状態
/// </summary>
public enum RacketState
{
    NORMAL,//通常状態
    SWING,//スイング状態
    REVERSE,//戻る状態
}

public class RacketMove : MonoBehaviour {

    //ラケット状態
    RacketState racketState = RacketState.NORMAL;
    //スイング時間
    public float originSwingTime = 1.0f;
    float swingTime;

	// Use this for initialization
	void Start () {
        //スイング時間初期化
        swingTime = originSwingTime;
	}
	
	// Update is called once per frame
	void Update () {
        //ラケットの状態で行動変化
        switch(racketState)
        {
            //通常状態
            case RacketState.NORMAL:
                //トリガーが押されたら
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    //ラケット状態変更
                    //racketState = RacketState.SWING;
                }
                break;

            //スイング状態
            case RacketState.SWING:
                //スイング時間減少
                swingTime -= Time.deltaTime;
                //スイング時間が0になったら
                if(swingTime <= 0)
                {
                    //スイング時間初期化
                    swingTime = originSwingTime;
                    //ラケット状態変更
                    racketState = RacketState.REVERSE;
                }
                break;

            //戻る状態
            case RacketState.REVERSE:
                //ラケット状態変更
                racketState = RacketState.NORMAL;
                break;
        }
	}
}
