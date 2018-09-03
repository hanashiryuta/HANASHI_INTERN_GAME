///
///製作日：2018/08/30
///作成者：葉梨竜太
///画面外の爆弾ヘルプUI
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSerchUIMove : MonoBehaviour {

    //視線中央
    public GameObject centerEyesPoint;
    //左側ヘルプUI
    public GameObject leftSerchUI;
    //右側ヘルプUI
    public GameObject rightSerchUI;
    //レイヤーマスク
    LayerMask layerMask;
    //壁管理クラス
    WallController wallController;

	// Use this for initialization
	void Start () {
        //視線がぶつかる判定用壁レイヤーマスク指定
        layerMask = LayerMask.GetMask(new string[] {"EyeWall" });
        //壁管理クラス
        wallController = GameObject.Find("Walls").GetComponent<WallController>();
	}
	
	// Update is called once per frame
	void Update () {
        //方向ベクトル取得
        Vector3 diff = centerEyesPoint.transform.forward;
        //y軸は回転させない
        diff.y = 0;
        //方向の絶対値が0以上なら
        if (diff.magnitude > 0.01f)
        {
            //自身を回転
            transform.rotation = Quaternion.LookRotation(diff);
        }
        //レイ生成
        Ray ray = new Ray(centerEyesPoint.transform.position, centerEyesPoint.transform.forward);
        //当たったオブジェクト
        RaycastHit hit;
        //視線の先にレイ飛ばす
        if (Physics.Raycast(ray,out hit, 100,layerMask))
        {
            //当たったオブジェクトのタグ
            string tags = hit.transform.tag;
            //タグによって処理変更
            switch(tags)
            {
                //正面の壁を見ている
                case "FRONTWALL":
                    //左の壁から爆弾が来てたら 
                    if (wallController.bombs[(int)WallType.LEFTWALL].Count >= 1)
                        //左側表示
                        leftSerchUI.SetActive(true);
                    //来てなければ
                    else
                        //左側非表示
                        leftSerchUI.SetActive(false);
                    //右の壁から爆弾が来てたら 
                    if (wallController.bombs[(int)WallType.RIGHTWALL].Count >= 1)
                        //右側表示
                        rightSerchUI.SetActive(true);
                    //来てなければ
                    else
                        //右側非表示
                        rightSerchUI.SetActive(false);
                    break;

                //左の壁を見ている
                case "LEFTWALL":
                    //左側非表示
                    leftSerchUI.SetActive(false);
                    //正面、または右の壁から爆弾が来てたら
                    if (wallController.bombs[(int)WallType.RIGHTWALL].Count >= 1|| wallController.bombs[(int)WallType.FRONTWALL].Count >= 1)
                        //右側表示
                        rightSerchUI.SetActive(true);
                    //来てなければ
                    else
                        //右側非表示
                        rightSerchUI.SetActive(false);
                    break;

                //右の壁を見ている
                case "RIGHTWALL":
                    //右側非表示
                    rightSerchUI.SetActive(false);
                    //正面、または左の壁から爆弾が来ていたら
                    if (wallController.bombs[(int)WallType.LEFTWALL].Count >= 1 || wallController.bombs[(int)WallType.FRONTWALL].Count >= 1)
                        //左側表示
                        leftSerchUI.SetActive(true);
                    //来ていなければ
                    else
                        //左側非表示
                        leftSerchUI.SetActive(false);                    
                    break;
            }
        }
        //どこも見ていなければ
        else
        {
            //右側非表示
            rightSerchUI.SetActive(false);
            //左側非表示
            leftSerchUI.SetActive(false);
        }
    }
}
