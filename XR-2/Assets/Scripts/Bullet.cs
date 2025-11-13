using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        // Destroy the bullet after 5 seconds
        Destroy(gameObject, 5f);
    }
}