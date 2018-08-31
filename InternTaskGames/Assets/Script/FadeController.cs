using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum FadeActionState
{
    SceneStart,
    Stay,
    SceneEnd,
}

public class FadeController : MonoBehaviour {

    public GameObject fadeImageObj;
    Image fadeImage;
    Color fadeColor;
    float alpha = 1.3f;

    public string nextScene;

    [HideInInspector]
    public bool isSceneStart;

    [HideInInspector]
    public bool isSceneEnd;

    [HideInInspector]
    public FadeActionState fadeActionState = FadeActionState.SceneStart;

	// Use this for initialization
	void Start () {
        fadeActionState = FadeActionState.SceneStart;
        fadeImage = fadeImageObj.GetComponent<Image>();
        fadeColor = fadeImage.color;
        fadeColor.a = alpha;
        fadeImage.color = fadeColor;
	}
	
	// Update is called once per frame
	void Update () {
        switch(fadeActionState)
        {
            case FadeActionState.SceneEnd:
                alpha += Time.deltaTime / 2;
                if (alpha >= 1.3f)
                {
                    //リザルトシーンへ移行
                    SceneManager.LoadScene(nextScene);
                }
                break;
            case FadeActionState.Stay:
                if (isSceneEnd)
                    fadeActionState = FadeActionState.SceneEnd;
                break;
            case FadeActionState.SceneStart:
                alpha -= Time.deltaTime / 2;
                if (alpha <= 0.0f)
                {
                    isSceneStart = true;
                    fadeActionState = FadeActionState.Stay;
                }
                break;
        }

        fadeColor.a = alpha;
        fadeImage.color = fadeColor;            
	}
}
