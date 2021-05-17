using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIScript : MonoBehaviour
{

    public RectTransform thrusterFuelFill;
    public RectTransform healthFill;

    private PlayerController playerController = null;

    void SetFuelAmount(float _amount) {
        thrusterFuelFill.localScale = new Vector3(1f, _amount, 1f);
    }

    void SetHealthAmount(float _amount) {
        healthFill.localScale = new Vector3(1f, _amount, 1f);
    }

    void DoStuff() {
        SetFuelAmount(playerController.GetFuelAmount());
        SetHealthAmount(playerController.GetHealthAmount());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController == null) {
            GameObject temp = GameObject.Find("Player(Clone)");
            if (temp != null) {
                playerController = temp.GetComponent<PlayerController>();
                DoStuff();
            }
        } else {
            DoStuff();
        }
    }
}
