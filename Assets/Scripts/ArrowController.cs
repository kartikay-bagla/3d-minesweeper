using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public int arrowDamage;
    public float maxLifetime;

    private void Die() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) {

        if(collision.collider.CompareTag("Player")) {
            collision.collider.GetComponent<PlayerController>().TakeDamage(arrowDamage);
        }
        Invoke("Die", 0.05f);
    }

    void Update()
    {
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) 
            Die();
    }
}
