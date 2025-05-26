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
        if(Input.GetKeyDown(KeyCode.K))
        {
            infoPanel.SetActive(!infoPanel.activeSelf);
        }
        if(infoPanel.activeSelf) ShowConnectionInfo();
    }
    private void ShowConnectionInfo()
    {
        if (infoTime > infoInterval)
        {
            infoTime = 0;
            var info = NetManager.GetConnectionInfo();
            var serverInfo = NetManager.GetServerInfo();
            if (serverInfo != null)
            {
                serverNameText.text = "Server Name:\n" + serverInfo.ServerName;
                addressText.text = "Address:\n" + serverInfo.Address + "\nPort: " + serverInfo.Port.ToString();
                currentPlayersText.text = "Players: " + serverInfo.CurrentPlayers.ToString() + " / " + serverInfo.MaxPlayers.ToString();
                gameModeText.text = "Game Mode: " + serverInfo.GameMode;
                customDataText.text = "Ping: " + info.Ping.ToString();
                pingText.text = "Connected Since: " + info.ConnectedSince.ToString();
                packetLossText.text = "Packet Loss: " + info.PacketLoss.ToString();
            }
        }
        infoTime += Time.deltaTime;
    }
}
