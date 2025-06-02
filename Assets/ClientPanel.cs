using TMPro;
using SimpleNet.Network;
using SimpleNet.Transport;
using UnityEngine;
using UnityEngine.UI;

public class ClientPanel : MonoBehaviour
{
    public GameObject serverTextPrefab;
    public GameObject hostPanel;
    private float time = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        NetManager.StartClient();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 1f)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            foreach (var discoveredServer in NetManager.GetDiscoveredServers())
            {
                Debug.Log(discoveredServer.ServerName);
                ServerInfo serverInfo = discoveredServer; 
                GameObject go = Instantiate(serverTextPrefab, transform);
                go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{serverInfo.ServerName}";
                go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Mode {serverInfo.GameMode}";
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"Max players {serverInfo.MaxPlayers}";
                Button button = go.transform.GetChild(3).GetComponent<Button>();
                if (button != null)
                {
                    // Capture current serverInfo in a local variable to avoid closure issue
                    ServerInfo capturedInfo = serverInfo;

                    button.onClick.AddListener(() => OnServerButtonClicked(capturedInfo));
                }
            }

            time = 0;
        } 
        time += Time.deltaTime;
        if (NetManager.Connected)
        {
            NetManager.StopLan();
            gameObject.SetActive(false);
            hostPanel.SetActive(true);
        }
    }

    private void OnServerButtonClicked(ServerInfo capturedInfo)
    {
        Debug.Log("OnServerButtonClicked: " + capturedInfo.ServerName);
        NetManager.ConnectTo(capturedInfo.Address);
    }
}
