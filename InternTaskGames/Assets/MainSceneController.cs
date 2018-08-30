using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour {

    FadeController fadeController;
    bool isSceneStart = false;
    bool isSceneEnd = false;

    void Start()
    {
        fadeController = GameObject.Find("Fade").GetComponent<FadeController>();
    }

    void Update()
    {
        if(fadeController.fadeActionState == FadeActionState.Stay)
        {
            SceneStart();
        }
        else if(fadeController.fadeActionState == FadeActionState.SceneEnd)
        {
            SceneEnd();
        }
    }

    public void SceneStart()
    {
        if (!isSceneStart)
        {
            TimeController.isTimeStart = true;
            EnemySpawnController.isSpawn = true;
            EnemyBombThrow.isThrow = true;
            isSceneStart = true;
        }
    }
    public void SceneEnd()
    {
        if (!isSceneEnd)
        {
            TimeController.isTimeStart = false;
            EnemySpawnController.isSpawn = false;
            EnemyBombThrow.isThrow = false;
            GameObject.Find("Walls").GetComponent<WallController>().BombsReset();
            ScoreController.SavedHighScore();
            isSceneEnd = true;
        }
    }
}
