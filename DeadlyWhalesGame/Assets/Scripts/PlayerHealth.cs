using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public TextMeshProUGUI healthTextTMP; // UI do wyświetlania zdrowia
    public GameObject gameOverUI;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return; // Ignoruj obrażenia, jeśli gracz jest martwy

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ustaw zdrowie w zakresie [0, maxHealth]
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthTextTMP != null)
        {
            healthTextTMP.text = $"HP: {currentHealth:F0}/{maxHealth}"; // Wyświetl zdrowie jako liczba całkowita
        }
        else
        {
            Debug.LogWarning("HealthTextTMP nie jest przypisany!");
        }
    }

    void Die()
{
    Debug.Log("Gracz zginął!");

    // Wyłączenie sterowania gracza
    WeaponShoot weaponShoot = GetComponent<WeaponShoot>();
    if (weaponShoot != null)
    {
        weaponShoot.enabled = false;
    }

    if (gameOverUI != null)
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Zatrzymanie gry

        // Wywołanie statystyk po śmierci
        GameOverManager gameOverManager = gameOverUI.GetComponent<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOverStats();
        }
    }
}


}
