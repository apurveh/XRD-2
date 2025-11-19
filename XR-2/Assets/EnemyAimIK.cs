using UnityEngine;

public class EnemyAimIK : MonoBehaviour
{
    // Assign your VR player's Main Camera
    public Transform playerCamera;

    private Animator enemyAnimator;

    // How much to look at the player (1 = 100%)
    [Range(0, 1)]
    public float lookAtWeight = 1.0f;

    void Start()
    {
        // Get the Animator that is on this same GameObject
        enemyAnimator = GetComponent<Animator>();
    }

    // This function will NOW be called because it's on the
    // same object as the Animator.
    void OnAnimatorIK(int layerIndex)
    {
        if (enemyAnimator == null || playerCamera == null)
        {
            return;
        }

        // Tell the Animator to aim the upper body at the player
        enemyAnimator.SetLookAtWeight(lookAtWeight);
        enemyAnimator.SetLookAtPosition(playerCamera.position);
    }
}