using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float damage = 20f;
    public float lifetime = 5f; // Czas życia pocisku
    public ParticleSystem hitEffect; // Efekt trafienia

    void Start()
    {
        Destroy(gameObject, lifetime); // Usunięcie pocisku po określonym czasie
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

            // Efekt trafienia
            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
