using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDisappearTrapScript : MonoBehaviour
{

    private Transform ground;
    private bool destroyed = false;

    void RemoveGround() {
        ground = transform.parent.Find("Ground");
        if (ground != null) {
            Destroy(ground.gameObject);
            destroyed = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RemoveGround();

    }

    // Update is called once per frame
    void Update()
    {
        if (!destroyed) {
            RemoveGround();
        } else {
            // Destroy(gameObject);
        }
    }
}
