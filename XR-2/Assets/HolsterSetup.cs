using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables; // Added for clarity
using UnityEngine.XR.Interaction.Toolkit.Interactors; // Added for clarity

public class HolsterSetup : MonoBehaviour
{
    [Header("Assign the Gun's XR Grab Interactable here")]
    public XRGrabInteractable gunInteractable;

    private XRSocketInteractor socketInteractor;

    void Start()
    {
        // We can use GetComponent without the full namespace path since we added the 'using' directives
        socketInteractor = GetComponent<XRSocketInteractor>();

        // Ensure both the socket and the gun are assigned before attempting the force grab
        if (socketInteractor != null && gunInteractable != null && socketInteractor.interactionManager != null)
        {
            // We move the gun to the socket's position first to prevent visual jump
            // Note: Use the attachTransform as the placement reference
            if (socketInteractor.attachTransform != null)
            {
                gunInteractable.transform.position = socketInteractor.attachTransform.position;
                gunInteractable.transform.rotation = socketInteractor.attachTransform.rotation;
            }
            else
            {
                // Fallback: Use the socket's own transform if attachTransform is not set
                gunInteractable.transform.position = socketInteractor.transform.position;
                gunInteractable.transform.rotation = socketInteractor.transform.rotation;
            }

            socketInteractor.interactionManager.SelectEnter(
                (IXRSelectInteractor)socketInteractor,
                (IXRSelectInteractable)gunInteractable
            );
        }
    }
}