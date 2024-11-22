using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f; // Maksymalne zdrowie przeciwnika
    private float currentHealth;
    private bool isDead = false; // Flaga, aby zapobiec wielokrotnemu liczeniu zabójstw

    void Start()
    {
        currentHealth = maxHealth; // Ustaw początkowe zdrowie
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return; // Jeśli przeciwnik jest już martwy, ignoruj obrażenia

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{gameObject.name} otrzymał obrażenia: {damage}. Aktualne zdrowie: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; // Zapobiega wielokrotnemu wywołaniu
        isDead = true;

        Debug.Log($"{gameObject.name} zginął!");

        // Dodanie likwidacji do licznika
        if (KillCounter.instance != null)
        {
            KillCounter.instance.AddKill();
        }

        Destroy(gameObject); // Zniszczenie przeciwnika
    }
}
