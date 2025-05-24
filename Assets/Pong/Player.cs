using System;
using System.Collections;
using System.Collections.Generic;
using NetPackage.Network;
using NetPackage.Synchronization;
using UnityEngine;

public class Player : NetBehaviour
{
    public int player;
    [SerializeField] private float speed = 5;
    [SerializeField] private float topLimit = 5f;
    [SerializeField] private float bottomLimit = -5f;
    [SerializeField] private float leftLimit = -5f;
    [SerializeField] private float rightLimit = 5f;
    [SerializeField] private Color color;

    private CharacterController controller;

    protected override void OnNetSpawn()
    {
        if (player-1 != NetManager.ConnectionId())
        {
            enabled = false;
            return;
        }
        Own(NetManager.ConnectionId());
        
        // Get or add CharacterController component
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            controller = gameObject.AddComponent<CharacterController>();
        }
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && transform.position.y < topLimit)
        {
            movement += Vector3.up;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && transform.position.y > bottomLimit)
        {
            movement += Vector3.down;
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > leftLimit)
        {
            movement += Vector3.left;
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < rightLimit)
        {
            movement += Vector3.right;
        }

        // Normalize movement vector to prevent faster diagonal movement
        if (movement.magnitude > 0)
        {
            movement.Normalize();
        }

        // Apply movement using CharacterController
        controller.Move(movement * speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        // Draw the movement boundary box
        Vector3 center = new Vector3(
            (leftLimit + rightLimit) / 2f,
            (topLimit + bottomLimit) / 2f,
            transform.position.z
        );
        
        Vector3 size = new Vector3(
            rightLimit - leftLimit,
            topLimit - bottomLimit,
            0.5f
        );
        
        Gizmos.color = color;
        Gizmos.DrawWireCube(center, size);
    }

    protected override void OnDisconnect()
    {
        Own(-1);
    }
}
