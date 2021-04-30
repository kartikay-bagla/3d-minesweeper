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
    private RoomController[,] roomControllers;

    GameObject room;
    List<GameObject> selectedEnemies;

    public Camera minimapCam;
    public GameObject Environment;

    public float difficulty=0.1f;

    public GameObject gameAreaDespawnerPrefab;

    // Builds Navmesh for AI
    void BuildNavMesh()
    {
        // Builds Nav Mesh
        foreach (Transform child in rooms[gridSize - 1, gridSize - 1].transform)
        {
            if (child.name == "Ground")
            {
                Debug.Log("Building NavMesh");
                child.GetComponent<NavMeshSurface>().BuildNavMesh();
            }
        }

        // Closes Doors
        Debug.Log("Closing Doors");
        foreach (GameObject room in rooms)
        {
            foreach (DoorController door in room.GetComponentsInChildren<DoorController>())
            {
                if (door != null) door.SetClosed();
            }
        }
    }

    // Assigns enemies and traps to rooms
    void RoomEnemyAssignment() {
        Debug.Log("Assigning Enemies To Room");
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

                RoomController roomController = room.GetComponent<RoomController>();

                if (roomController != null)
                {
                    roomController.AssignEnemies(selectedEnemies);
                }
                else
                {
                    Debug.Log("Room Controller is Null");
                }

                if (Random.Range(0f, 1f) < difficulty)
                {
                    roomController.tag = "Trap";
                    roomController.isMine = true;
                };
                room.name = "Room " + i + ", " + j;


            }
        }
    }
    
    // Assign Value to each cell for number of traps nearby
    private void LabelGrid()
    {
        for (int x = 0; x < gridSize; x++) {
			for (int z = 0; z < gridSize; z++) {
				if (!roomControllers[x,z].isMine)
                {
                    for (int dx=-1; dx<=1; dx++)
                        for (int dz=-1; dz<=1; dz++)
                            if (x+dx>=0 && x+dx<gridSize && z+dz>=0 && z+dz<gridSize && roomControllers[x+dx, z+dz].isMine)
                                roomControllers[x,z].gridValue++;
                }
			}
		}
    }

    // Spawns player
    void SpawnPlayer() {
        Debug.Log("Spawning Player");

        GameObject playerInstance = Instantiate(player, new Vector3(20, 1, 0), Quaternion.identity);
        playerInstance.GetComponent<PlayerMotor>().minimapCam = minimapCam;
    }

    // Create rooms
    void SpawnRooms() {
        int _startX = 0;
        int _startZ = 0;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                rooms[i, j] = Instantiate(roomPrefab, new Vector3(_startX, 0, _startZ), Quaternion.identity);
                rooms[i, j].transform.parent = Environment.transform;

                roomControllers[i, j] = rooms[i, j].GetComponent<RoomController>();
                _startX += increment;
            }
            _startX = 0;
            _startZ += increment;
        }
    }

    // Initializes traps, enemies and player
    void InitializeEverything() {
        BuildNavMesh();
        RoomEnemyAssignment();
        LabelGrid();
        SpawnPlayer();
    }

    // Called to begin the game
    void BeginGame() {
        rooms = new GameObject[gridSize, gridSize];
        roomControllers = new RoomController[gridSize, gridSize];
        selectedEnemies = new List<GameObject>();
        SpawnGameAreaDespawner();
        SpawnRooms();
        Invoke("InitializeEverything", 0.01f);
    }

    private void SpawnGameAreaDespawner()
    {
        Instantiate(gameAreaDespawnerPrefab, Vector3.zero, Quaternion.identity);
    }

    void Start()
    {
        BeginGame();
    }
}
