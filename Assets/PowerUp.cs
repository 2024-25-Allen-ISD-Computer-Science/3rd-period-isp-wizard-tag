using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerupEffect powerupEffect;
    public float bobSpeed = 2f; // Speed of the bobbing motion
    public float bobHeight = 0.35f; // Height of the bobbing motion

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position; // Store the starting position
    }

    private void Update()
    {
        // Apply a sine wave to the y position
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * bobSpeed) * bobHeight, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (powerupEffect != null)
        {
            powerupEffect.Apply(collision.gameObject);
        }
        Destroy(gameObject); // Destroy the powerup after it is picked up
    }
}
