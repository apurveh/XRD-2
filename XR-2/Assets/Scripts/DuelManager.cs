using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class DuelManager : MonoBehaviour
{
    [Header("Game Objects")]
    public XRGrabInteractable playerGun; // 'DW_Set0'
    public Animator enemyAnimator;       // The enemy's Animator component

    [Header("Audio")]
    public AudioClip bellSound;        // The "DRAW!" sound
    public AudioClip loseSound;        // A "bang" or "you lose" sound
    private AudioSource audioSource;

    [Header("Game Settings")]
    public float minWaitTime = 3.0f;
    public float maxWaitTime = 8.0f;
    public float enemyDrawTime = 1.5f; // << ENEMY SPEED! Time you have to shoot
    public float resetDelay = 4.0f;    // Time before a new duel starts

    // --- Game State ---
    private bool isWaiting = false;
    private bool canDraw = false;
    private bool gameIsOver = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (playerGun != null)
        {
            playerGun.selectEntered.AddListener(OnGunGrabbed);
        }
        else
        {
            Debug.LogError("DuelManager: Player Gun is not assigned!");
        }

        // Ensure enemy is visible at start
        if (enemyAnimator != null)
        {
            enemyAnimator.gameObject.SetActive(true);
        }

        // Begin the first duel!
        StartCoroutine(DuelRoutine());
    }

    void OnDestroy()
    {
        if (playerGun != null)
        {
            playerGun.selectEntered.RemoveListener(OnGunGrabbed);
        }
    }

    private void OnGunGrabbed(SelectEnterEventArgs args)
    {
        if (isWaiting)
        {
            // --- FOUL! ---
            Debug.Log("FOUL! You grabbed the gun too early!");
            PlayerLoses("Foul! Too early!");
        }
    }

    IEnumerator DuelRoutine()
    {
        // --- 1. The "Waiting" Phase ---
        gameIsOver = false;
        isWaiting = true;
        canDraw = false;

        // Ensure enemy is awake and idling for the next round
        if (enemyAnimator != null)
        {
            enemyAnimator.gameObject.SetActive(true);
            // Optional: Reset trigger if it was stuck (good practice)
            enemyAnimator.ResetTrigger("Draw");
        }

        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        Debug.Log("Waiting for " + waitTime + " seconds... Don't grab!");
        yield return new WaitForSeconds(waitTime);

        // --- 2. The "DRAW!" Phase ---
        isWaiting = false;
        canDraw = true;

        Debug.Log("DRAW!!!");

        if (bellSound != null)
        {
            audioSource.PlayOneShot(bellSound);
        }

        // --- TELL ENEMY TO DRAW! ---
        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Draw");
        }

        // --- 3. START THE ENEMY'S TIMER! ---
        StartCoroutine(EnemyShotTimer());
    }

    // This is the "enemy's" countdown
    IEnumerator EnemyShotTimer()
    {
        yield return new WaitForSeconds(enemyDrawTime);

        // If we got here, the player was too slow.
        if (!gameIsOver)
        {
            PlayerLoses("Too slow! You were shot!");
        }
    }

    // This is called by the Target script when you hit it
    public void PlayerWins()
    {
        if (gameIsOver) return;

        gameIsOver = true;
        Debug.Log("--- YOU WIN! ---");

        // Stop the enemy from shooting you
        StopCoroutine("EnemyShotTimer");

        // Disable enemy immediately on hit (replace with death animation later)
        if (enemyAnimator != null)
        {
            enemyAnimator.gameObject.SetActive(false);
        }

        // Start a new duel after a delay
        StartCoroutine(ResetDuel("You win! Next round..."));
    }

    // This is called if you foul or are too slow
    private void PlayerLoses(string message)
    {
        if (gameIsOver) return;

        gameIsOver = true;
        StopAllCoroutines(); // Stop the "Waiting" coroutine

        Debug.Log("--- YOU LOSE! --- " + message);

        if (loseSound != null)
        {
            audioSource.PlayOneShot(loseSound);
        }

        // Start a new duel after a delay
        StartCoroutine(ResetDuel("You lose. Next round..."));
    }

    // Resets the game for the next round
    IEnumerator ResetDuel(string message)
    {
        Debug.Log(message);
        yield return new WaitForSeconds(resetDelay);

        // Start a new round
        StartCoroutine(DuelRoutine());
    }
}