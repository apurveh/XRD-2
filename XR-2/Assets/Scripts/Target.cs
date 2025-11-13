using UnityEngine;

public class Target : MonoBehaviour
{
    // We need a reference to the main manager
    public DuelManager duelManager;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the thing that hit us has the "Bullet" tag
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Destroy the bullet
            Destroy(collision.gameObject);

            // --- TELL THE MANAGER YOU WON ---
            if (duelManager != null)
            {
                duelManager.PlayerWins(); // Call the win function!
            }

            // Make the target disappear
            gameObject.SetActive(false);
        }
    }
}