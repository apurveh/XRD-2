using UnityEngine;

public class EnemyVisuals : MonoBehaviour
{
    public GameObject holsterGun;
    public GameObject handGun;

    // --- ADD THESE VARIABLES ---
    public AudioClip shootSound;
    private AudioSource audioSource;

    void Start()
    {
        // Find or add an AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void SwapToHandGun()
    {
        if (holsterGun != null) holsterGun.SetActive(false);
        if (handGun != null) handGun.SetActive(true);
    }

    public void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    // ... (ResetToHolster remains the same) ...
    public void ResetToHolster()
    {
        if (holsterGun != null) holsterGun.SetActive(true);
        if (handGun != null) handGun.SetActive(false);
    }
}