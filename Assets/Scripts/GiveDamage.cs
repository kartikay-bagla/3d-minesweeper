using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    public int damageAmount = 10;
    public float damageTime = 0.5f;
    bool isPlayerinDamageRadius;
    float lastDamageTime;
    PlayerController playerControllerToDamage;
    
    void Start()
    {
        isPlayerinDamageRadius = false;
        lastDamageTime = Time.time;
    }

    void DamagePlayer()
    {

        // Check if player in radius
        //   if he in radius, check if enough time has passed to damage player
        //     if yes then check if controller is set and damage player
        //     else call damageplayer after enough time has passed
        //   else set playercontrollertodamage to null

        if (isPlayerinDamageRadius)
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
            else
            {
                Invoke("DamagePlayer", lastDamageTime + damageTime - Time.time);
            }
        }
        else
        {
            playerControllerToDamage = null;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerinDamageRadius = true;
            playerControllerToDamage = collider.GetComponent<PlayerController>();
            DamagePlayer();
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.CompareTag("Player"))
        {
            isPlayerinDamageRadius = false;
        }
    }

    void Update()
    {
        
    }
}
