using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f; // Maksymalne zdrowie przeciwnika
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Ustawienie początkowego zdrowia
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Odejmowanie obrażeń
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Ignoruj kolizje z graczem, chyba że jest to pocisk
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Kolizja z graczem, ale brak obrażeń.");
            return;
        }
    }

    void Die()
    {
        Debug.Log("Przeciwnik zginął!");
        Destroy(gameObject); // Zniszczenie przeciwnika
    }
}
