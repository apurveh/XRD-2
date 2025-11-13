using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables; // For XRGrabInteractable

public class GunController : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab; // The 'Bullet45' prefab
    public Transform muzzlePoint;     // The empty "MuzzlePoint" object at the barrel tip
    public float bulletSpeed = 50f;

    [Header("Ammo")]
    public int maxAmmo = 6;
    // Assign your 6 'DW_Bullet_X' objects from the Hierarchy
    public GameObject[] magazineBullets;

    [Header("Audio")]
    public AudioClip gunshotSound; // Your trimmed gunshot sound file
    private AudioSource audioSource;   // The AudioSource component on this object

    private int currentAmmo;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        // Get the components on this same GameObject
        grabInteractable = GetComponent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource

        // Add a listener to the "activated" event (this is the trigger pull)
        grabInteractable.activated.AddListener(Shoot);

        // Load the gun
        Reload();
    }

    // Clean up the listener when the object is destroyed
    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.activated.RemoveListener(Shoot);
        }
    }

    // This function is called when the trigger is pulled
    public void Shoot(ActivateEventArgs args)
    {
        // Check if we have ammo
        if (currentAmmo > 0)
        {
            // --- 1. Decrement Ammo ---
            currentAmmo--;

            // --- 2. Play Sound ---
            if (gunshotSound != null)
            {
                // PlayOneShot is best for rapid sounds
                audioSource.PlayOneShot(gunshotSound);
            }

            // --- 3. Fire the Bullet ---
            GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            // Add force along the bullet's 'up' (Y) direction
            rb.AddForce(bullet.transform.up * bulletSpeed, ForceMode.Impulse);

            // --- 4. Update Visuals ---
            // Hide the corresponding bullet model in the magazine
            if (magazineBullets.Length > currentAmmo)
            {
                magazineBullets[currentAmmo].SetActive(false);
            }
        }
        else
        {
            // (This is where we'll add an 'empty click' sound)
        }
    }

    // A simple reload function to reset ammo
    public void Reload()
    {
        currentAmmo = maxAmmo;
        // Show all the bullet models in the magazine
        foreach (GameObject bulletModel in magazineBullets)
        {
            if (bulletModel != null)
            {
                bulletModel.SetActive(true);
            }
        }
    }
}