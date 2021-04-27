using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject explosion;

    public int bulletDamage;
    public float maxLifetime;

    int collisions;

    private void Setup() {
        collisions = 0;
    }

    private void Die() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider) {

        // Debug.Log(collider.gameObject.name);

        if(collider.CompareTag("Character")) {
            // Debug.Log(collider.name);
            collider.GetComponent<AIController>().TakeDamage(bulletDamage);
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
        if (collisions > 0) Die();

        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Die();
    }
}
