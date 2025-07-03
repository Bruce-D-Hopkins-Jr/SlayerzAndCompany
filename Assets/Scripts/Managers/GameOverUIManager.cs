using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverUIManager : MonoBehaviour 
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverMessage;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        gameOverPanel.SetActive(false);

        restartButton.onClick.AddListener(RestartRun);
        quitButton.onClick.AddListener(ReturnToMainMenu);
    }

    public void ShowGameOver(bool isVictory)
    {
        gameOverPanel.SetActive(true);

        if (isVictory)
        {
            gameOverMessage.text = "VICTORY!";
        }
        else
        {
            gameOverMessage.text = "DEFEAT!";
        }

        Debug.Log($"[GameOverUIManager] Game over shown. Victory: {isVictory}");
    }

    private void RestartRun()
    {
        SceneManager.LoadScene("TestScene");
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("SlayingMenu");
    }
}
