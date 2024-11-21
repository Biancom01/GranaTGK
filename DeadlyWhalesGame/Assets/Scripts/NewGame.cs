using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    // Referencja do przycisku
    public Button startNewGameButton;

    void Start()
    {
        // Dodajemy listener do przycisku
        startNewGameButton.onClick.AddListener(StartNewGame);
    }

    // Funkcja zmieniaj¹ca scenê
    void StartNewGame()
    {
        // Prze³¹czamy scenê na "GameScene"
        SceneManager.LoadScene("GameScene");
    }
}
