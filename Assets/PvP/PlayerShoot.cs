using System;
using NetPackage.Network;
using NetPackage.Synchronization;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShoot : NetBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float fireRate = 0.5f;

    private float nextFireTime;
    [FormerlySerializedAs("mainCamera")] public Camera shootCamera;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isOwned)
            return;
        // Check if enough time has passed since last shot and if fire button is pressed
        if (nextFireTime >= fireRate && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        nextFireTime += Time.deltaTime;
    }

    
    private void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            CallRPC("SpawnBullet");
        }
    }

    [NetRPC]
    private void SpawnBullet()
    {
        if(NetManager.IsHost)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Vector3 worldCenter = shootCamera.ScreenToWorldPoint(new Vector3(screenCenter.x, screenCenter.y, 10f));
            Quaternion rotationTowardsCenter = Quaternion.LookRotation(worldCenter - firePoint.position);
            NetManager.Spawn(bulletPrefab, firePoint.position, rotationTowardsCenter, NetManager.ConnectionId());
        }
        animator.SetTrigger("Move");
        nextFireTime = 0;
    }
    
} 