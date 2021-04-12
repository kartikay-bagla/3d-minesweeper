using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject roomPrefab;
    public GameObject player;
    public int gridSize = 10;
    public int increment = 2;

    void Start()
    {
        int _startX = 0;
        int _startZ = 0;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Instantiate(roomPrefab, new Vector3(_startX, 0, _startZ), Quaternion.identity);
                _startX += increment;
            }
            _startX = 0;
            _startZ += increment;
        }

        Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);

    }
}
