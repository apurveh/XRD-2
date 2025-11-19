using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRAvatarRig : MonoBehaviour
{
    // The player's head (VR Camera)
    public Transform headTarget;
    // The avatar's hands (Animator)
    public Animator avatarAnimator;

    // The player's hands (XR Controllers)
    public Transform leftHandTarget;
    public Transform rightHandTarget;

    // Offset to make the avatar slightly taller than the camera
    public Vector3 headBodyOffset;

    void LateUpdate()
    {
        // 1. Position the Body Root (Moves the Hips/Legs)
        // Position the avatar based on the head's horizontal position
        transform.position = headTarget.position + headBodyOffset;

        // 2. Rotate the Body to match the Head (Y-axis only)
        // This makes the whole body turn with the player's view
        transform.forward = Vector3.ProjectOnPlane(headTarget.forward, Vector3.up).normalized;
    }

    // Called after the animation update
    void OnAnimatorIK(int layerIndex)
    {
        if (avatarAnimator == null) return;

        // --- CORRECTED HEAD/LOOKAT IK ---
        // We use SetLookAt functions, which automatically adjust the neck and spine.
        avatarAnimator.SetLookAtWeight(1.0f);
        avatarAnimator.SetLookAtPosition(headTarget.position);

        // --- Left Hand IK ---
        avatarAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        avatarAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        avatarAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
        avatarAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);

        // --- Right Hand IK ---
        avatarAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        avatarAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        avatarAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
        avatarAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
    }
}