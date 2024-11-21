using UnityEngine;

public class EnemyDash : MonoBehaviour
{
    public Transform player; // Referencja do gracza
    public float normalSpeed = 2f; // Normalna prędkość ruchu
    public float dashSpeed = 10f; // Prędkość podczas dasha
    public float dashRange = 5f; // Minimalna odległość do aktywacji dasha
    public float detectionRange = 15f; // Maksymalna odległość wykrycia gracza
    public float dashCooldown = 5f; // Czas między dashami
    public float stunDuration = 2f; // Czas trwania stuna po dashu
    public float slowSpeed = 1f; // Prędkość w trakcie stuna

    private float cooldownTimer = 0f; // Licznik czasu cooldownu
    private bool isStunned = false; // Czy przeciwnik jest w stanie stuna
    private float stunTimer = 0f; // Licznik czasu stuna

    private void Start()
    {
        // Automatyczne znalezienie gracza
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Nie znaleziono gracza w scenie!");
        }
    }

    private void Update()
    {
        if (player == null) return; // Wyjdź, jeśli nie ma gracza

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isStunned)
        {
            HandleStun();
        }
        else if (cooldownTimer > 0f)
        {
            HandleCooldown();
        }
        else if (distanceToPlayer <= dashRange)
        {
            DashTowardsPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer(normalSpeed); // Normalny ruch w kierunku gracza
        }
    }

    private void MoveTowardsPlayer(float speed)
    {
        // Ruch w stronę gracza z daną prędkością
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Zablokowanie ruchu na osi Y
        transform.position += direction * speed * Time.deltaTime;

        // Obrót w stronę gracza
        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void DashTowardsPlayer()
    {
        // Dash w kierunku gracza
        Vector3 dashDirection = (player.position - transform.position).normalized;
        dashDirection.y = 0; // Zablokowanie osi Y

        transform.position += dashDirection * dashSpeed * Time.deltaTime;

        // Obrót w stronę gracza
        transform.rotation = Quaternion.LookRotation(dashDirection);

        // Rozpocznij stun i cooldown
        isStunned = true;
        stunTimer = stunDuration;
        cooldownTimer = dashCooldown;
    }

    private void HandleStun()
    {
        // Przeciwnik porusza się wolniej podczas stuna
        stunTimer -= Time.deltaTime;

        if (stunTimer <= 0f)
        {
            isStunned = false;
        }
        else
        {
            MoveTowardsPlayer(slowSpeed);
        }
    }

    private void HandleCooldown()
    {
        // Licznik czasu cooldownu
        cooldownTimer -= Time.deltaTime;

        // Podczas cooldownu, jeśli gracz jest w zasięgu, przeciwnik porusza się normalnie
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            MoveTowardsPlayer(normalSpeed);
        }
    }
}
