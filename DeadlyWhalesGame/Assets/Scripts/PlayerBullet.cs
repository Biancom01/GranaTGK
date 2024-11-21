using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float damage = 20f; // Obrażenia zadawane przeciwnikom

    void OnTriggerEnter(Collider other)
    {
        // Sprawdź, czy pocisk trafił przeciwnika
        if (other.CompareTag("Enemy"))
        {
            // Zadaj obrażenia przeciwnikowi
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Zniszcz pocisk po trafieniu
            Destroy(gameObject);
        }

        // Nie rób nic, jeśli pocisk trafi gracza lub inne obiekty
    }
}
