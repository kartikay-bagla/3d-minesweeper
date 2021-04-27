using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public Room roomPrefab;
    public int sizeX, sizeZ;
    public int increment = 2;
    public float difficulty=0.1f;
    private Room[,] rooms;
    void Start()
    {
        
    }

    public void Generate () {
		rooms = new Room[sizeX, sizeZ];
		for (int x = 0; x < sizeX; x++) {
			for (int z = 0; z < sizeZ; z++) {
				CreateRoom(x, z);
			}
		}
        LabelGrid();
	}

    private void LabelGrid()
    {
        for (int x = 0; x < sizeX; x++) {
			for (int z = 0; z < sizeZ; z++) {
				if (!rooms[x,z].isMine)
                {
                    for (int dx=-1; dx<=1; dx++)
                        for (int dz=-1; dz<=1; dz++)
                            if (x+dx>=0 && x+dx<sizeX && z+dz>=0 && z+dz<sizeZ && rooms[x+dx, z+dz].isMine)
                                rooms[x,z].gridValue++;
                }
			}
		}
    }

	private void CreateRoom (int x, int z) {
	    Room newRoom = Instantiate(roomPrefab, new Vector3(x*increment, 0, z*increment), Quaternion.identity) as Room;
        newRoom.transform.parent = transform;
        if (Random.Range(0f,1f)<difficulty)
        {
            newRoom.tag = "Trap";
            newRoom.isMine=true;
        }
		rooms[x, z] = newRoom;
		newRoom.name = "Room " + x + ", " + z;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
