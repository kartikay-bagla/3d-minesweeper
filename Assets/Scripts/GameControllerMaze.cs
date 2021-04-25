using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerMaze : MonoBehaviour
{
    public GameObject player;
    public GameObject mazePrefab;
	private GameObject mazeInstance;

    void Start()
    {
        BeginGame();
    }

    private void BeginGame() { 
        mazeInstance = Instantiate(mazePrefab) as GameObject;
        mazeInstance.GetComponent<MazeGenerator>().Generate();
        Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);
    }
}
