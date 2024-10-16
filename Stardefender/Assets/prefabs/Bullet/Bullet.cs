using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        // Destroy the bullet after 2 seconds
        Destroy(gameObject, 2f);
    }
}

