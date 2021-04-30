using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaDespawnerScript : MonoBehaviour
{

    public int floorDamage = 100000;

    void OnTriggerExit(Collider col) {
        if (col.name == "Player") {
            col.GetComponent<PlayerController>().TakeDamage(floorDamage);
        } else {
            Destroy(col.gameObject);
        }
    }
}
