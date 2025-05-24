using System;
using System.Collections;
using System.Collections.Generic;
using NetPackage.Network;
using NetPackage.Synchronization;
using UnityEngine;

public class Health : NetBehaviour
{
    [Sync] public int lifes = 3;
    [Header("Combat Settings")]
    [SerializeField] private float invulnerabilityDuration = 1.5f;
    private bool isInvulnerable = false;
    private float invulnerabilityTimer = 0f;
    private Vector3 spawnPosition;

    protected override void OnNetStart()
    {
        spawnPosition = transform.position;
    }

    private void Update()
    {
        
        UpdateInvulnerability();
    }

    public void Hit()
    {
        if (!isInvulnerable)
        {
            lifes--;
            if (lifes <= 0)
                CallRPC("Die");
            else
            {
                isInvulnerable = true;
                invulnerabilityTimer = invulnerabilityDuration;
            }
        }
    }

    [NetRPC(Direction.ServerToClient)]
    private void Die()
    {
        GetComponent<NetTransform>().PausePrediction();
        GetComponentInChildren<NetTransform>().PausePrediction();
        transform.position = spawnPosition;
        GetComponent<NetTransform>().ResumePrediction();
        GetComponentInChildren<NetTransform>().ResumePrediction();
        lifes = 3;
    }
    
    private void UpdateInvulnerability()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer <= 0f)
            {
                isInvulnerable = false;
            }
        }
    }
}
