using System;
using System.Collections;
using System.Collections.Generic;
using NetPackage.Network;
using NetPackage.Synchronization;
using TMPro;
using UnityEngine;

public class PointCount : NetBehaviour
{
    public TextMeshProUGUI pointsTxt;
    [SerializeField] private Material material;
    [SerializeField] private GameObject fondo;

    private int points = 5;


    private void OnCollisionEnter(Collision other)
    {
        if(NetManager.IsHost)
            CallRPC("SumPoint");
    }

    [NetRPC(Direction.ServerToClient, Send.All)]
    private void SumPoint()
    {
        points--;
        pointsTxt.text = points.ToString();
        fondo.GetComponent<MeshRenderer>().material = material;
    }
}
