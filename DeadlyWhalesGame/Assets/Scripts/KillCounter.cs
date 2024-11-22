using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public static KillCounter instance; // Singleton do łatwego dostępu
    public TextMeshProUGUI killsText; // UI do licznika zabitych przeciwników
    public TextMeshProUGUI survivedTimeText; // UI do wyświetlania czasu przetrwania

    private int killCount = 0;
    private float startTime;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        startTime = Time.time; // Rejestrujemy czas startu gry
        UpdateKillUI();
    }

    void Update()
    {
        // Aktualizowanie czasu przetrwania w czasie rzeczywistym
        if (survivedTimeText != null)
        {
            float survivalTime = Time.time - startTime;
            survivedTimeText.text = $"Survived: {survivalTime:F1}s";
        }
    }

    public void AddKill()
    {
        killCount++;
        UpdateKillUI();
    }

    public int GetKillCount()
    {
        return killCount;
    }

    public float GetSurvivalTime()
    {
        return Time.time - startTime;
    }

    private void UpdateKillUI()
    {
        if (killsText != null)
        {
            killsText.text = $"Kills: {killCount}";
        }
    }
}
