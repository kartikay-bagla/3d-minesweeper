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

    [SerializeField]
    private RoomController roomController;

    private bool openDoor = false;
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

        SetOpen();
    }

    public void SetOpen() {
        Vector3 posOrigLoc = posDoor.transform.localPosition;
        posDoor.transform.localPosition = new Vector3(posOrigLoc.x, posOpenY, posOrigLoc.z);

        Vector3 negOrigLoc = negDoor.transform.localPosition;
        negDoor.transform.localPosition = new Vector3(negOrigLoc.x, negOpenY, negOrigLoc.z);

        openDoor = true;
    }

    public void SetClosed() {
        Vector3 posOrigLoc = posDoor.transform.localPosition;
        posDoor.transform.localPosition = new Vector3(posOrigLoc.x, posClosedY, posOrigLoc.z);

        Vector3 negOrigLoc = negDoor.transform.localPosition;
        negDoor.transform.localPosition = new Vector3(negOrigLoc.x, negClosedY, negOrigLoc.z);

        openDoor = false;
    }

    void FixedUpdate()
    {
        if (openDoor) {
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

    private void UpdateOpenDoor() {
        if (numEntitiesInside > 0) {
            openDoor = true;
        } else {
            openDoor = false;
        }
    }

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Character" || col.gameObject.tag == "Player") {
            numEntitiesInside += 1;
        }
        UpdateOpenDoor();
        roomController.EntityEnteredDoorSensor(col);
    }

    private void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "Character" || col.gameObject.tag == "Player") {
            numEntitiesInside -= 1;
        }
        UpdateOpenDoor();
        roomController.EntityLeftDoorSensor(col);
    }
}
