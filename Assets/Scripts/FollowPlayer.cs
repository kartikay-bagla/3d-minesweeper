using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        Invoke("FindPlayer", 0.1f);
    }

    void FindPlayer ()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void Update()
    {
        if (player==null)
        {
            GameObject[] list = GameObject.FindGameObjectsWithTag("Player");
            if (list.Length>0)
                player = list[0];
        }
        else
            transform.position = new Vector3(player.transform.position.x, 71, player.transform.position.z);
    }
}
