using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;    // The bullet prefab to shoot
    public Transform bulletSpawnPoint; // Where the bullet spawns
    public float bulletSpeed = 10f;    // Speed of the bullet

    private AudioSource bulletSound;   // AudioSource to play the firing sound
    private PlayerControls controls;   // Input controls

    void Awake()
    {
        // Initialize input controls
        controls = new PlayerControls();

        // Bind the Fire action to the Shoot function
        controls.Player.Fire.performed += ctx => Shoot();

        // Get the AudioSource component from the Player
        bulletSound = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void Shoot()
    {
        // Instantiate the bullet at the player's position
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Set bullet velocity
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawnPoint.forward * bulletSpeed;

        // Play the firing sound
        PlayFireSound();
    }

    void PlayFireSound()
    {
        if (bulletSound != null)
        {
            // Play the sound attached to the AudioSource in the Inspector
            bulletSound.Play();
        }
        else
        {
            Debug.LogError("AudioSource component is missing!");
        }
    }
}
