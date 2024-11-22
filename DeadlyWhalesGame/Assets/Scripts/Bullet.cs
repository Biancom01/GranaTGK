using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; // Obrażenia pocisku
    public float speed = 20f; // Prędkość pocisku
    public float lifetime = 5f; // Czas życia pocisku

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false; // Wyłączenie grawitacji
            rb.velocity = transform.forward * speed; // Nadanie stałej prędkości w kierunku pocisku
        }

        Destroy(gameObject, lifetime); // Usuń pocisk po określonym czasie
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Zniszcz pocisk po trafieniu
        }

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Zniszcz pocisk po trafieniu
        }
    }
}
