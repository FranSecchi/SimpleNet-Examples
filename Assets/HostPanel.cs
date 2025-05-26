using NetPackage.Network;
using NetPackage.Transport;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HostPanel : MonoBehaviour
{
    private string NameScene;
    public GameObject verticalLayoutGroup;
    public TextMeshProUGUI ServerNameText;
    public TextMeshProUGUI MaxPlayersText;
    public TMP_InputField inputField;
    public Button startGameButton;

    public GameObject playerTextPrefab;
    // Start is called before the first frame update
    void Start()
    {
        ServerInfo info = null;
        switch (MenuSelection.mode)
        {
            case GameMode.Pong:
                info = new ServerInfo()
                {
                    ServerName = "Server Pong",
                    GameMode = "Pong",
                    MaxPlayers = 4
                };
                NameScene = "Pong";
                break;
            case GameMode.PvP:
                info = new ServerInfo()
                {
                    ServerName = "Server PvP",
                    GameMode = "PvP",
                    MaxPlayers = 8
                };
                NameScene = "PvP";
                break;
            case GameMode.Test:
                info = new ServerInfo()
                {
                    ServerName = "Server Ping",
                    GameMode = "Ping",
                    MaxPlayers = 4
                };
                NameScene = "Test";
                break;
                
        }
        if(MenuSelection.isHost) NetManager.StartHost(info);
        else StartClient();
        MaxPlayersText.text = NetManager.MaxPlayers.ToString();
        startGameButton.enabled = false;
    }

    private void StartClient()
    {
        startGameButton.gameObject.SetActive(false);
        inputField.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (Transform child in verticalLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }
        ServerNameText.text = NetManager.ServerName;

        if (NetManager.AllPlayers.Count > 1)
        {
            var info = NetManager.GetServerInfo();
            for(int i = 1; i <= NetManager.PlayerCount; i++)
            {
                GameObject go = Instantiate(playerTextPrefab, verticalLayoutGroup.transform);
                TextMeshProUGUI text = go.GetComponent<TextMeshProUGUI>();
                text.text = $"Player {i}";
            }
            if(NetManager.IsHost) startGameButton.enabled = NetManager.AllPlayers.Count > 1;
        }
    }

    public void OnClickStartGame()
    {
        NetManager.StopLan();
        NetManager.LoadScene(NameScene);
    }
    public void OnEditName()
    {
        string serverName = inputField.text;
        ServerNameText.text = serverName;
        NetManager.SetServerName(serverName);
    }
}
