using SimpleNet.Network;
using SimpleNet.Synchronization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : NetBehaviour
{
    public MeshRenderer renderer;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI StatusText;
    public TextMeshProUGUI HostText;
    public Button ColorButton;
    public Button PingButton;
    
    public int playerNum;
    [Sync]
    private string playerName;
    private int playerId;

    protected override void OnNetSpawn()
    {
        if (playerNum  > NetManager.PlayerCount)
        {
            ColorButton.enabled = false;
            return;
        }
        playerId = NetManager.ConnectionId();
        if(playerId == playerNum - 2)
            Own(playerId);
        if(NetManager.IsHost && playerId == playerNum - 2)
            HostText.text = "Host";
        else HostText.text = "Client";
    }

    protected override void OnNetEnable()
    {
        renderer.material.color = ColorButton.image.color;
        playerName = "Player " + playerNum;
        NameText.text = playerName;
    }


    public void OnColorButtonClick()
    {
        if(!IsOwned)
            CallRPC("RequestColorChange", playerId + 2);
    }
    public void OnPingButtonClick()
    {
        if(NetManager.IsHost)
            CallRPC("PingAllPlayers");
    }
    
    [NetRPC]
    private void PingAllPlayers()
    {
        StatusText.text = $"Pinged by host!";
        renderer.material.color = ColorButton.image.color;
    }

    [NetRPC]
    private void RequestColorChange(int id)
    {
        if (!NetManager.IsHost)
            return;
        Color newColor = new Color(
            UnityEngine.Random.value,
            UnityEngine.Random.value,
            UnityEngine.Random.value
        );
        CallRPC("SetPlayerColor",newColor, id);
    }

    [NetRPC(Direction.ServerToClient)]
    private void SetPlayerColor(Color color, int id)
    {
        renderer.material.color = color;
        StatusText.text = $"Last ping from player {id}!";
    }

}
