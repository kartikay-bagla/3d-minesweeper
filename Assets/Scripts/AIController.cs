using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{


    [SerializeField]
    private Transform goal;

    private NavMeshAgent playerAgent;

    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }

    void Update() 
    {
        playerAgent.destination = goal.position;
    }
}
