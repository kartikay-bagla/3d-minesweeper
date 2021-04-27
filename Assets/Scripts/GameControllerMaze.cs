using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerMaze : MonoBehaviour
{
    public GameObject player;
    public GameObject mazePrefab;
    public Camera minimapCam;
	private GameObject mazeInstance;
    private GameObject Environment;

    void Start()
    {
        BeginGame();
    }

    private void BeginGame() { 
        Environment = GameObject.Find("Environment");
        mazeInstance = Instantiate(mazePrefab) as GameObject;
        mazeInstance.transform.parent = Environment.transform;
        mazeInstance.GetComponent<MazeGenerator>().Generate();
        GameObject playerInstance = Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);
        playerInstance.GetComponent<PlayerMotor>().minimapCam = minimapCam;
    }
}
