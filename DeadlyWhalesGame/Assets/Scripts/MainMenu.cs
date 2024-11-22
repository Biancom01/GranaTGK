using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Wczytaj scenę gry (upewnij się, że scena gry nazywa się "GameScene")
    }

    public void QuitGame()
    {
        Application.Quit(); // Wyjdź z gry (działa tylko w buildzie)
        Debug.Log("Gra zamknięta.");
    }
}
