using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; // Referencja do gracza
    private NavMeshAgent agent;

    void Start()
    {
        // Znajdujemy NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        // Jeœli nie przypisaliœmy gracza w edytorze, szukamy go automatycznie po tagu "Player"
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        // Ustawiamy cel agenta na pozycjê gracza
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}
