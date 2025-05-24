using System.Collections;
using System.Collections.Generic;
using NetPackage.Network;
using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    public float infoInterval;
    public GameObject infoPanel;
    public TextMeshProUGUI pingText;
    public TextMeshProUGUI packetLossText;
    public TextMeshProUGUI serverNameText;
    public TextMeshProUGUI addressText;
    public TextMeshProUGUI currentPlayersText;
    public TextMeshProUGUI gameModeText;
    public TextMeshProUGUI customDataText;
    private float infoTime = 0f;

    // Update is called once per frame
    void Update()
    {
        ShowConnectionInfo();
    }
    private void ShowConnectionInfo()
    {
        if (infoTime > infoInterval)
        {
            infoTime = 0;
            var info = NetManager.GetConnectionInfo();
            pingText.text = "Ping: " + info.Ping.ToString() + " | Connected Since: " + info.ConnectedSince.ToString();
            packetLossText.text = "Packet Loss: " + info.PacketLoss.ToString();
            var serverInfo = NetManager.GetServerInfo();
            if (serverInfo != null)
            {
                serverNameText.text = "Server Name: " + serverInfo.ServerName + " | Ping: " + serverInfo.Ping.ToString();
                addressText.text = "Address: " + serverInfo.Address + " | Port: " + serverInfo.Port.ToString();
                currentPlayersText.text = "Players: " + serverInfo.CurrentPlayers.ToString() + " / " + serverInfo.MaxPlayers.ToString();
                gameModeText.text = "Game Mode: " + serverInfo.GameMode;
                if (serverInfo.CustomData != null && serverInfo.CustomData.Count > 0)
                {
                    customDataText.text = "Custom Data: " + string.Join("\n", serverInfo.CustomData);
                }
                else
                {
                    customDataText.text = "Custom Data: ";
                }
            }
        }
        infoTime += Time.deltaTime;
    }
}
