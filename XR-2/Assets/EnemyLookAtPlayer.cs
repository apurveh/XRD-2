using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    // Assign your VR player's Main Camera to this in the Inspector
    [SerializeField]
    private Transform playerCamera;

    void Update()
    {
        if (playerCamera != null)
        {
            // --- This is the key part ---

            // 1. Get the player's position
            Vector3 targetPosition = playerCamera.position;

            // 2. Force the target's 'y' (height) to be the same as the enemy's.
            // This prevents the enemy from tilting up or down.
            targetPosition.y = transform.position.y;

            // 3. Look at that modified position
            transform.LookAt(targetPosition);
        }
    }
}