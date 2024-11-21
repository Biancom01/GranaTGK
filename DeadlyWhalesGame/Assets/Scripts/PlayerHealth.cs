using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maksymalne zdrowie
    private float currentHealth; // Aktualne zdrowie

    public TextMeshProUGUI healthTextTMP; // UI pokazujące zdrowie
    public GameObject gameOverUI; // UI ekranu końcowego

    void Start()
    {
        currentHealth = maxHealth; // Ustawienie początkowego zdrowia
        UpdateHealthUI();

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false); // Ukryj ekran końcowy na początku gry
        }
        else
        {
            Debug.LogError("GameOverUI nie jest przypisany w inspektorze!");
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"Gracz otrzymał obrażenia: {damage}"); // Diagnostyka obrażeń
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
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
            healthTextTMP.text = $"HP: {currentHealth}/{maxHealth}";
        }
        else
        {
            Debug.LogWarning("HealthTextTMP nie jest przypisany w inspektorze!");
        }
    }

    void Die()
    {
        Debug.Log("Gracz zginął!");
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Pokaż ekran końcowy
            Time.timeScale = 0f; // Zatrzymaj czas gry
        }
        else
        {
            Debug.LogError("GameOverUI nie jest przypisany w inspektorze!");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Przywróć czas gry
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Wczytaj bieżącą scenę
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Przywróć czas gry
        SceneManager.LoadScene("MainMenu"); // Wczytaj scenę menu głównego
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Gracz został trafiony przez: {other.name}"); // Diagnostyka kolizji

        if (other.CompareTag("EnemyBullet"))
        {
            float bulletDamage = 10f; // Domyślna wartość obrażeń pocisku
            TakeDamage(bulletDamage);
            Destroy(other.gameObject); // Zniszcz pocisk po kolizji
        }
    }
}
