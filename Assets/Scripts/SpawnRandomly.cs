using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomly : MonoBehaviour
{
    public float xRange;
    public float yRange;
    public float zRange;
    public GameObject laserBeam;
    public int noOfShooters = 20;
    void Start()
    {
        Vector3 randomPosition;
        for (int i=0; i<noOfShooters; i++)
        {
            randomPosition = new Vector3(
                Random.Range(-xRange/2, xRange/2), 
                Random.Range(-yRange/2, yRange/2),
                Random.Range(-zRange/2, zRange/2)
            );
            GameObject laser = Instantiate(laserBeam, transform);
            laser.transform.position+= randomPosition; 
            laser.transform.rotation = Random.rotation; 
        }
    }
}
