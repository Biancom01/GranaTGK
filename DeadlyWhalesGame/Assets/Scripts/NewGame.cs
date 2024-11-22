using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    public Button startNewGameButton;

    void Start()
    {
        if (startNewGameButton != null)
            startNewGameButton.onClick.AddListener(StartNewGame);
        else
            Debug.LogError("Przycisk startNewGameButton nie jest przypisany!");
    }

    void StartNewGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
