using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectile;
    public float launchVelocity = 700f;
    public float timeBetweenShooting = 0.5f;
    public float spread = 1f;
    
    bool readyToShoot;
    bool allowInvoke;

    void Start()
    {
        readyToShoot=true;
        allowInvoke=true;
    }

    void Shoot()
    {
        readyToShoot = false;

        GameObject arrow = Instantiate(projectile, transform.position, transform.rotation, transform);

        float x = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);

        arrow.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(x, launchVelocity,z));

        if (allowInvoke) {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
    }

    private void ResetShot() {
        readyToShoot = true;
        allowInvoke = true;
    }   

    void Update()
    {
        if (readyToShoot)
            Shoot();
    }
}
