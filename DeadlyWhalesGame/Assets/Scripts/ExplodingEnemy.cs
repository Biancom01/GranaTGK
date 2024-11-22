using UnityEngine;

public class ExplodingEnemy : MonoBehaviour
{
    public GameObject explosionEffect; // Prefab efektu wybuchu
    public float explosionRadius = 5f; // Promień wybuchu
    public float explosionDamage = 50f; // Obrażenia wybuchu
    public float speed = 3f; // Prędkość ruchu
    private Transform player; // Referencja do gracza

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Podążanie w kierunku gracza
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Jeśli dotknie gracza, wybucha
        if (other.CompareTag("Player"))
        {
            Explode();
        }
    }

   void Explode()
{
    // Efekt wizualny eksplozji
    if (explosionEffect != null)
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Zniszczenie efektu po zakończeniu
        ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            Destroy(explosion, ps.main.duration); // Usuwamy po czasie trwania efektu
        }
        else
        {
            Destroy(explosion, 3f); // Dla bezpieczeństwa, usuń po 3 sekundach jeśli brak ParticleSystem
        }
    }

    // Zadanie obrażeń graczowi i innym w promieniu
    Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
    foreach (Collider nearbyObject in colliders)
    {
        if (nearbyObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = nearbyObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(explosionDamage);
            }
        }
    }

    Destroy(gameObject); // Zniszczenie przeciwnika
}

}
