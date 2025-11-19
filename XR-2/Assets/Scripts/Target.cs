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

            // --- FIX: REMOVED gameObject.SetActive(false); ---
            // We rely on the DuelManager to reset the enemy after the animation.

            // OPTIONAL: Disable the Collider so you can't hit him again while he's dying
            Collider targetCollider = GetComponent<Collider>();
            if (targetCollider != null)
            {
                targetCollider.enabled = false;
            }
        }
    }
}