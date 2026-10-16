using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int collectedCoins = 0;
    public int totalCoins = 6;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI coinText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1f;
        if (winPanel) winPanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);
        UpdateCoinText();
    }

    public void AddCoin()
    {
        collectedCoins++;
        UpdateCoinText();

        if (collectedCoins >= totalCoins)
        {
            WinGame();
        }
    }

    void UpdateCoinText()
    {
        if (coinText)
        {
            coinText.text = "Monedas: " + collectedCoins + " / " + totalCoins;
        }
    }

    public void WinGame()
    {
        if (winPanel) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        if (gameOverPanel) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // Función para reiniciar el juego al presionar un botón
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}