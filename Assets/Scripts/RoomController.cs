using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{
    public GameObject[] trapPrefabs;

    List<Transform> spawnPoints;
    Transform[] chosenSpawnLocations;

    bool playerInDoorSensor;
    bool playerInRoom;

    GameObject player;

    List<GameObject> roomEnemies;
    bool activated;

    public bool isMine = false;
    public int gridValue = 0;

    private bool hasEntered = false;

    void OnTriggerEnter(Collider collider) {
        if (!hasEntered && collider.tag == "Player")
        {
            hasEntered = true;
            if (isMine)
                TriggerTrap();
            else
                DisplayValueOfRoom();
        }
    }

    void TriggerTrap()
    {
        if (trapPrefabs.Length==0)
            return;
        int index = Random.Range(0, trapPrefabs.Length);
        GameObject trap = Instantiate(trapPrefabs[index], transform);
    }

    void DisplayValueOfRoom()
    {
        Text value = GetComponentInChildren(typeof(Text)) as Text;
        value.text = gridValue.ToString();
    }

    public void EntityEnteredDoorSensor(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Player entered door of room");
            playerInDoorSensor = true;
        }
    }

    public void EntityLeftDoorSensor(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInDoorSensor = false;
        }
    }

    void CreateSpawnPoints()
    {
        spawnPoints = new List<Transform>();

        foreach (Transform child in transform)
        {
            if (child != null && child.name == "EnemySpawnPoints")
            {
                foreach (Transform sp in child)
                {
                    if (sp != null) spawnPoints.Add(sp);
                }
            }
        }

        chosenSpawnLocations = new Transform[spawnPoints.Count];

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            // Take only from the latter part of the list - ignore the first i items.
            int take = Random.Range(i, spawnPoints.Count);
            chosenSpawnLocations[i] = spawnPoints[take];

            // Swap our random choice to the beginning of the array,
            // so we don't choose it again on subsequent iterations.
            spawnPoints[take] = spawnPoints[i];
            spawnPoints[i] = chosenSpawnLocations[i];
        }
    }

    public void AssignEnemies(List<GameObject> enemies)
    {
        roomEnemies = enemies;

        if (chosenSpawnLocations == null)
        {
            CreateSpawnPoints();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        if (chosenSpawnLocations == null)
        {
            CreateSpawnPoints();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            if (playerInDoorSensor)
            {
                Debug.Log("Player in room, enemies in room: " + roomEnemies.Count as string);
                if (roomEnemies.Count > 0)
                {

                    for (int i = 0; i < roomEnemies.Count; i++)
                    {
                        Instantiate(roomEnemies[i], chosenSpawnLocations[i].position, Quaternion.identity);
                    }

                    activated = true;
                }
            }
        }
    }
}
