using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSerchUIMove : MonoBehaviour {

    public GameObject centerEyesPoint;
    public GameObject leftSerchUI;
    public GameObject rightSerchUI;
    LayerMask layerMask;
    WallController wallController;

	// Use this for initialization
	void Start () {
        layerMask = LayerMask.GetMask(new string[] {"EyeWall" });
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

        Ray ray = new Ray(centerEyesPoint.transform.position, centerEyesPoint.transform.forward);
        
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit, 100,layerMask))
        {
            string tags = hit.transform.tag;
            switch(tags)
            {
                case "FRONTWALL":
 
                    if (wallController.bombs[(int)WallType.LEFTWALL].Count >= 1)
                        leftSerchUI.SetActive(true);
                    else
                        leftSerchUI.SetActive(false);

                    if (wallController.bombs[(int)WallType.RIGHTWALL].Count >= 1)
                        rightSerchUI.SetActive(true);
                    else
                        rightSerchUI.SetActive(false);

                    break;
                case "LEFTWALL":
                    leftSerchUI.SetActive(false);
                    if (wallController.bombs[(int)WallType.RIGHTWALL].Count >= 1|| wallController.bombs[(int)WallType.FRONTWALL].Count >= 1)
                        rightSerchUI.SetActive(true);
                    else
                        rightSerchUI.SetActive(false);

                    break;
                case "RIGHTWALL":
                    rightSerchUI.SetActive(false);
                    if (wallController.bombs[(int)WallType.LEFTWALL].Count >= 1 || wallController.bombs[(int)WallType.FRONTWALL].Count >= 1)
                        leftSerchUI.SetActive(true);
                    else
                        leftSerchUI.SetActive(false);
                    
                    break;
            }
        }
        else
        {
            rightSerchUI.SetActive(false);
            leftSerchUI.SetActive(false);
        }
    }
}
