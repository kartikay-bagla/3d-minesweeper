using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    GameObject goal;

    NavMeshAgent playerAgent;

    [SerializeField]
    int maxHealth = 100;
    int currentHealth;

    bool isPlayerinDamageRadius;
    PlayerController playerControllerToDamage;

    [SerializeField]
    int enemyDamage;

    [SerializeField]
    int damageTime;
    float lastDamageTime;

    public void TakeDamage(int explosionDamage)
    {
        currentHealth -= explosionDamage;
        Debug.Log(currentHealth);
    }

    private void Die()
    {
        Destroy(gameObject);
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
                    playerControllerToDamage.TakeDamage(enemyDamage);
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

    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;
        isPlayerinDamageRadius = false;

        lastDamageTime = Time.time;

        goal = GameObject.Find("Player(Clone)");

    }

    void Update()
    {

        // Debug.Log("CanMoveEnemyUpdate: " + canEnemyMoveRandomShit as string);

        // // Activate();

        // Debug.Log("CanMoveEnemyUpdateAfter: " + canMove as string);

        if (goal == null)
        {
            goal = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            playerAgent.destination = goal.transform.position;
        }



        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
