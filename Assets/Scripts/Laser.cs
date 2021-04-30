using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int maxLength = 5000;
    public int damageAmount = 1;
    public float damageTime = 0.01f;
    float lastDamageTime;
    PlayerController playerControllerToDamage;
    private LineRenderer lr;

	void Start () {
        lr = GetComponent<LineRenderer>();
        lastDamageTime = Time.time;
	}

	void Update () {
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
                if (hit.collider.tag=="Player")
                {
                    playerControllerToDamage = hit.collider.GetComponent<PlayerController>();
                    DamagePlayer();
                }
            }
        }
        else 
            lr.SetPosition(1, transform.forward*maxLength);
	}

    void DamagePlayer()
    {
        if (Time.time > lastDamageTime + damageTime)
        {
            if (playerControllerToDamage != null)
            {
                playerControllerToDamage.TakeDamage(damageAmount);
                Invoke("DamagePlayer", damageTime);
                lastDamageTime = Time.time;
            }
        }
    }
}
