using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{

    public GameObject roomPrefab;
    public GameObject[] enemyPrefabs;
    public GameObject player;
    public int gridSize = 10;
    public int increment = 2;

    private GameObject[,] rooms;

    GameObject room;
    List<GameObject> selectedEnemies;

    // void BuildNavMesh() {
    //     NavMeshSurface[] navMeshSurfaces = gameObject.GetComponentsInChildren<NavMeshSurface>(true);

    //     Debug.Log("We have " + navMeshSurfaces.Length as string + " surfaces");

    //     foreach (NavMeshSurface navMeshSurface in navMeshSurfaces)
    //     {
    //         navMeshSurface.BuildNavMesh();
    //     }
    // }

    void BuildNavMesh()
    {
        foreach (Transform child in rooms[gridSize - 1, gridSize - 1].transform)
        {
            if (child.name == "Ground")
            {
                child.GetComponent<NavMeshSurface>().BuildNavMesh();
            }
        }

        foreach (GameObject room in rooms)
        {
            foreach (DoorController door in room.GetComponentsInChildren<DoorController>())
                {
                    if (door != null) door.SetClosed();
                }
        }


        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {

                room = rooms[i, j];

                selectedEnemies.Clear();
                for (int z = 0; z < UnityEngine.Random.Range(0, 6); z++)
                {
                    GameObject temp = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
                    selectedEnemies.Add(temp);
                }

                // Debug.Log("Spawning " + selectedEnemies.Count as string + " enemies");

                RoomController roomController = room.GetComponent<RoomController>();

                if (roomController != null)
                {
                    roomController.AssignEnemies(selectedEnemies);
                }
                else
                {
                    Debug.Log("Room Controller is Null");
                }


            }
        }

        Instantiate(player, new Vector3(20, 1, 0), Quaternion.identity);
    }

    void Start()
    {

        rooms = new GameObject[gridSize, gridSize];
        selectedEnemies = new List<GameObject>();

        int _startX = 0;
        int _startZ = 0;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                rooms[i, j] = Instantiate(roomPrefab, new Vector3(_startX, 0, _startZ), Quaternion.identity);

                _startX += increment;
            }

            _startX = 0;
            _startZ += increment;
        }

        

        Invoke("BuildNavMesh", 1f);

    }
}
