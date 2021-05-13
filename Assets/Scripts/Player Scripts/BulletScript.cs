using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject explosion;

    public int bulletDamage;
    public float maxLifetime;
    public int maxCollisions;

    int collisions;

    private void Setup() {
        collisions = 0;
    }

    private void Die() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider) {

        if(collider.CompareTag("Character")) {
            // Debug.Log(collider.name);
            collider.GetComponent<AIController>().TakeDamage(bulletDamage);
            Debug.Log(collider.name);
            Invoke("Die", 0.05f);
        }
        collisions += 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (collisions > maxCollisions) Die();

        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Die();
    }
}
