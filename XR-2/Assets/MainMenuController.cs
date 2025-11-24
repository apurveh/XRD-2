using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        UpdateHighScoreDisplay();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("QuickdrawScene");
    }

    public void LoadTutorial()
    {
        Debug.Log("Tutorial coming soon...");
    }

    private void UpdateHighScoreDisplay()
    {
        if (highScoreText == null) return;

        float bestTime = PlayerPrefs.GetFloat("BestReactionTime", 1000.0f);

        if (bestTime >= 1000.0f)
        {
            highScoreText.text = "Best Time: --";
        }
        else
        {
            highScoreText.text = $"Best Time: {bestTime.ToString("F2")}s";
        }
    }
}