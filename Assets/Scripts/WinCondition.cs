using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    GameController gameController;
    MaintainScore scoreScript;
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        scoreScript = GameObject.Find("Score (Number)").GetComponent<MaintainScore>();
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") 
        {
            scoreScript.AddScore(100);
            gameController.NextLevel();
        }
    }
}
