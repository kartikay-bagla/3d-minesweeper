using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    public int sizeX, sizeZ;
    public int increment = 2;
    public float difficulty=0.1f;
    private GameObject[,] rooms;
    void Start()
    {
        
    }

    public void Generate () {
		rooms = new GameObject[sizeX, sizeZ];
		for (int x = 0; x < sizeX; x++) {
			for (int z = 0; z < sizeZ; z++) {
				CreateRoom(x, z);
			}
		}
	}

	private void CreateRoom (int x, int z) {
		GameObject newRoom = Instantiate(roomPrefab, new Vector3(x*increment, 0, z*increment), Quaternion.identity) as GameObject;
        newRoom.transform.parent = transform;
        if (Random.Range(0f,1f)<difficulty)
            newRoom.tag = "Trap";
		rooms[x, z] = newRoom;
		newRoom.name = "Room " + x + ", " + z;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
