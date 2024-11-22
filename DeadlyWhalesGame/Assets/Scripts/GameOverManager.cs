using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI survivalTimeText;

    public void ShowGameOverStats()
    {
        // Pobieramy dane z KillCounter
        int kills = KillCounter.instance.GetKillCount();
        float survivalTime = KillCounter.instance.GetSurvivalTime();

        // Wyświetlamy dane
        if (killsText != null)
        {
            killsText.text = $"Kills: {kills}";
        }

        if (survivalTimeText != null)
        {
            survivalTimeText.text = $"Survived: {survivalTime:F1}s";
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Przywrócenie czasu gry
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart poziomu
    }

    public void QuitGame()
    {
        Application.Quit(); // Wyjście z gry
    }
}
