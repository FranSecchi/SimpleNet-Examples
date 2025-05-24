using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetPackage.Runtime
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float initialSpeed = 5f;
        [SerializeField] private float speedIncrease = 0.5f;
        [SerializeField] private float maxSpeed = 20f;
        [SerializeField] private float collisionCooldown = 0.1f;
        private float currentSpeed;
        private Vector3 direction;
        private Rigidbody rb;
        private float lastCollisionTime;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            // Disable gravity
            rb.useGravity = false;
            // Initialize speed
            currentSpeed = initialSpeed;
            // Start with random direction
            SetRandomDirection();
            lastCollisionTime = -collisionCooldown; // Allow first collision immediately
        }

        private void Update()
        {
            // Move the ball using transform, keeping Z position constant
            Vector3 movement = direction * currentSpeed * Time.deltaTime;
            movement.z = 0; // Ensure no movement in Z axis
            transform.Translate(movement);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Calculate reflection direction based on the collision normal
            Vector3 normal = collision.contacts[0].normal;
            // Project the normal onto XY plane
            normal.z = 0;
            normal = normal.normalized;
            
            // Reflect the direction and ensure it stays in XY plane
            direction = Vector3.Reflect(direction, normal);
            direction.z = 0;
            direction = direction.normalized;

            // Only increase speed if enough time has passed since last collision
            if (Time.time - lastCollisionTime >= collisionCooldown)
            {
                currentSpeed = Mathf.Min(currentSpeed + speedIncrease, maxSpeed);
                lastCollisionTime = Time.time;
            }
        }

        private void SetRandomDirection()
        {
            // Generate random angle between 0 and 360 degrees
            float randomAngle = UnityEngine.Random.Range(0f, 360f);
            // Convert angle to radians and create direction vector
            direction = new Vector3(
                Mathf.Cos(randomAngle * Mathf.Deg2Rad),
                Mathf.Sin(randomAngle * Mathf.Deg2Rad),
                0f
            ).normalized;
        }
    }
}
