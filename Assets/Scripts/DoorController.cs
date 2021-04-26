using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField]
    private GameObject posDoor;

    [SerializeField]
    private GameObject negDoor;

    [SerializeField]
    private float moveDistance = 0.005f;

    [SerializeField]
    private float speed = 5f;

    private bool playerIsHere = false;
    private int numEntitiesInside = 0;

    private float posClosedY;
    private float negClosedY;

    private float posOpenY;
    private float negOpenY;

    // Start is called before the first frame update
    void Start()
    {
        posClosedY = posDoor.transform.localPosition.y;
        negClosedY = negDoor.transform.localPosition.y;

        posOpenY = posClosedY - moveDistance;
        negOpenY = negClosedY + moveDistance;
    }

    void FixedUpdate()
    {
        if (playerIsHere) {
            // move doors until open
            if (Mathf.Approximately(posDoor.transform.localPosition.y, posOpenY) || posDoor.transform.localPosition.y >= posOpenY) {
                posDoor.transform.Translate(0f, 0f, speed * Time.fixedDeltaTime);
            }

            if (Mathf.Approximately(negDoor.transform.localPosition.y, negOpenY) || negDoor.transform.localPosition.y <= negOpenY) {
                negDoor.transform.Translate(0f, 0f, -speed * Time.fixedDeltaTime);
            }
        } else {
            // move doors until closed
            if (Mathf.Approximately(posDoor.transform.localPosition.y, posClosedY) || posDoor.transform.localPosition.y <= posClosedY) {
                posDoor.transform.Translate(0f, 0f, -speed * Time.fixedDeltaTime);
            }

            if (Mathf.Approximately(negDoor.transform.localPosition.y, negClosedY) || negDoor.transform.localPosition.y >= negClosedY) {
                negDoor.transform.Translate(0f, 0f, speed * Time.fixedDeltaTime);
            }
        }
    }

    private void UpdatePlayerIsHere() {
        if (numEntitiesInside > 0) {
            playerIsHere = true;
        } else {
            playerIsHere = false;
        }
    }

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Character" || col.gameObject.tag == "Player") {
            numEntitiesInside += 1;
        }
        UpdatePlayerIsHere();
    }

    private void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "Character" || col.gameObject.tag == "Player") {
            numEntitiesInside -= 1;
        }
        UpdatePlayerIsHere();
    }
}
