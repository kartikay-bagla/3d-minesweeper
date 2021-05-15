using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToGoal : MonoBehaviour
{
    private RectTransform pointerRectTransform;
    void Start()
    {
        pointerRectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        Vector3 targetPosition = GameObject.Find("Teleporter").transform.position;
        Vector3 fromPosition = Camera.main.transform.position;
        targetPosition.y=fromPosition.y=0;
        Vector3 dir = (targetPosition-fromPosition).normalized;
        float angle = (Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg) % 360;
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
        pointerRectTransform.anchoredPosition3D = 10*new Vector3(dir.x, dir.z, 0).normalized;
    }
}
