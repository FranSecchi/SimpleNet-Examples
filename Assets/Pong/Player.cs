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

    protected override void OnNetSpawn()
    {
        if (player-1 != NetManager.ConnectionId())
        {
            enabled = false;
            return;
        }
        Own(NetManager.ConnectionId());
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && transform.position.y < topLimit)
        {
            transform.Translate(Vector3.up * (speed * Time.deltaTime));
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && transform.position.y > bottomLimit)
        {
            transform.Translate(Vector3.down * (speed * Time.deltaTime));
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > leftLimit)
        {
            transform.Translate(Vector3.left * (speed * Time.deltaTime));
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < rightLimit)
        {
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }
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
