using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; // Referencja do gracza
    private NavMeshAgent agent;

    [SerializeField]
    private float speed = 3.5f; // Prędkość poruszania się przeciwnika
    [SerializeField]
    private float stoppingDistance = 2.0f; // Minimalna odległość od gracza, na której zatrzyma się przeciwnik

    [SerializeField]
    private float rotationSpeed = 5f; // Prędkość rotacji w stronę gracza

    void Start()
    {
        // Znajdujemy komponent NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        // Ustawiamy prędkość i dystans zatrzymania przeciwnika
        agent.speed = speed; // Ustawienie prędkości poruszania się
        agent.stoppingDistance = stoppingDistance; // Odległość, przy której przeciwnik się zatrzyma

        // Jeśli nie przypisano gracza, szukamy go automatycznie po tagu "Player"
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Ustawiamy NavMesh, aby śledził gracza
        agent.SetDestination(player.position);
    }

    void Update()
    {
        // Śledzimy gracza, dopóki nie jesteśmy w odpowiedniej odległości
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Jeżeli przeciwnik jest dalej niż minimalna odległość, podążaj za graczem
            if (distanceToPlayer > stoppingDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                // Zatrzymujemy przeciwnika, gdy jest blisko gracza
                agent.ResetPath();
                RotateTowardsPlayer();
            }
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0; // Obracanie tylko w osi poziomej

        // Płynna rotacja w stronę gracza
        if (directionToPlayer.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
