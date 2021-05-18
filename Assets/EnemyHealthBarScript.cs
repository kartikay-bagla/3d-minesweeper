using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarScript : MonoBehaviour
{

    public RectTransform healthFill;

    public AIController aiController;

    void SetHealthAmount(float _amount) {
        healthFill.localScale = new Vector3(-1f * _amount, 0.1f, 1f);
    }

    void Setup() {
        // healthFill = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiController != null) {
            SetHealthAmount(aiController.GetHealthAmount());
        }
    }
}
