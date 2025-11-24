using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using TMPro;

public class DuelManager : MonoBehaviour
{
    [Header("Game Objects")]
    public XRGrabInteractable playerGun;
    public Animator enemyAnimator;
    public EnemyVisuals enemyVisuals;

    [Header("UI Elements")]
    public GameObject dontGrabText;
    public GameObject shootText;
    public TextMeshProUGUI resultText;

    [Header("Audio")]
    public AudioClip bellSound;
    public AudioClip loseSound;
    public AudioClip winSound;
    private AudioSource audioSource;

    [Header("Game Settings")]
    public float minWaitTime = 3.0f;
    public float maxWaitTime = 8.0f;
    public float enemyDrawTime = 1.5f;

    // --- Game State ---
    private bool isWaiting = false;
    private bool gameIsOver = false;
    private float drawStartTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (playerGun != null)
            playerGun.selectEntered.AddListener(OnGunGrabbed);

        if (enemyAnimator != null)
            enemyAnimator.gameObject.SetActive(true);

        // Turn off all texts initially
        if (dontGrabText != null) dontGrabText.SetActive(false);
        if (shootText != null) shootText.SetActive(false);
        if (resultText != null) resultText.gameObject.SetActive(false);

        StartCoroutine(DuelRoutine());
    }

    void OnDestroy()
    {
        if (playerGun != null)
            playerGun.selectEntered.RemoveListener(OnGunGrabbed);
    }

    private void OnGunGrabbed(SelectEnterEventArgs args)
    {
        if (isWaiting)
        {
            PlayerLoses("FOUL! Too early!");
        }
    }

    IEnumerator DuelRoutine()
    {
        // 1. Setup Phase
        gameIsOver = false;
        isWaiting = true;

        if (dontGrabText != null) dontGrabText.SetActive(true);

        // Reset Enemy
        if (enemyAnimator != null)
        {
            enemyAnimator.gameObject.SetActive(true);
            enemyAnimator.ResetTrigger("Draw");
            enemyAnimator.ResetTrigger("Die");
        }
        if (enemyVisuals != null) enemyVisuals.ResetToHolster();

        // 2. Wait Phase
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);

        // 3. DRAW Phase
        isWaiting = false; // Gun is now live!

        if (dontGrabText != null) dontGrabText.SetActive(false);
        if (shootText != null) shootText.SetActive(true);

        // Mark the time RIGHT NOW
        drawStartTime = Time.time;

        if (bellSound != null) audioSource.PlayOneShot(bellSound);

        // Enemy Reaction Delay
        float reactionDelay = Random.Range(0.2f, 0.5f);
        yield return new WaitForSeconds(reactionDelay);

        if (enemyAnimator != null) enemyAnimator.SetTrigger("Draw");

        // Start Enemy Kill Timer
        StartCoroutine(EnemyShotTimer());
    }

    IEnumerator EnemyShotTimer()
    {
        yield return new WaitForSeconds(enemyDrawTime);
        if (!gameIsOver)
        {
            PlayerLoses("Too slow! You died.");
        }
    }

    public void PlayerWins()
    {
        if (gameIsOver) return;
        gameIsOver = true;

        StopCoroutine("EnemyShotTimer");

        // --- 1. Calculate Score ---
        float reactionTime = Time.time - drawStartTime;
        string reactionString = reactionTime.ToString("F2");

        // --- 2. Handle High Score (PlayerPrefs) ---
        // We use 1000.0f as a default if no score exists yet, because in speed, lower is better.
        float bestTime = PlayerPrefs.GetFloat("BestReactionTime", 1000.0f);
        string message = "";

        if (reactionTime < bestTime)
        {
            PlayerPrefs.SetFloat("BestReactionTime", reactionTime);
            PlayerPrefs.Save();
            message = $"VICTORY!\nNew Record: {reactionString}s!";
        }
        else
        {
            message = $"VICTORY!\nTime: {reactionString}s\nBest: {bestTime.ToString("F2")}s";
        }

        // --- 3. Update Visuals ---
        if (enemyAnimator != null) enemyAnimator.SetTrigger("Die");
        if (winSound != null) audioSource.PlayOneShot(winSound);

        StartCoroutine(EndGameSequence(message, true));
    }

    private void PlayerLoses(string reason)
    {
        if (gameIsOver) return;
        gameIsOver = true;
        StopAllCoroutines();

        if (loseSound != null) audioSource.PlayOneShot(loseSound);

        string message = $"DEFEAT\n{reason}";
        StartCoroutine(EndGameSequence(message, false));
    }

    IEnumerator EndGameSequence(string resultMessage, bool won)
    {
        // Hide game hints
        if (dontGrabText != null) dontGrabText.SetActive(false);
        if (shootText != null) shootText.SetActive(false);

        // Show Result
        if (resultText != null)
        {
            resultText.text = resultMessage;
            resultText.gameObject.SetActive(true);

            // Optional: Make text Green for win, Red for lose
            resultText.color = won ? Color.green : Color.red;
        }

        Debug.Log(resultMessage);

        // Wait 5 seconds so they can read it
        yield return new WaitForSeconds(5.0f);

        // Go back to menu
        SceneManager.LoadScene("MainMenu");
    }
}