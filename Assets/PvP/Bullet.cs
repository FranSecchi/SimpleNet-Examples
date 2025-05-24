using System;
using System.Collections;
using System.Collections.Generic;
using NetPackage.Network;
using NetPackage.Synchronization;
using UnityEngine;

public class Bullet : NetBehaviour
{
    public float speed;
    [SerializeField] private float lifetime = 5f; // Time in seconds before bullet is destroyed
    private Rigidbody rb;

    protected override void OnNetStart()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        // Start lifetime countdown
        StartCoroutine(DestroyAfterLifetime());
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        if (NetManager.IsHost)
        {
            NetManager.Destroy(NetID);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (NetManager.IsHost)
        {
            if (other.TryGetComponent(out Health component))
                component.Hit();
            NetManager.Destroy(NetID);
        }
    }
}
