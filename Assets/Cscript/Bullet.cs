// File: Bullet.cs
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;  // Bullet speed
    public float lifetime = 2f;      // Bullet lifetime

    void Start()
    {
        // Move the bullet forward at a constant speed
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;

        // Destroy the bullet after a set lifetime
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Destroy wall on collision and destroy bullet
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }
}
