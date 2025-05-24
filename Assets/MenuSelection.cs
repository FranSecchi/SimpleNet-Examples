using NetPackage.Network;
using UnityEngine;

public enum GameMode
{
    Client,
    Pong,
    PvP,
    Test
}
public class MenuSelection : MonoBehaviour
{
    public GameObject panel;
    public GameObject hostMenu;
    public GameObject clientMenu;
    public static GameMode mode = GameMode.PvP;
    public static bool isHost = false;

    public void OnPong()
    {
        mode = GameMode.Pong;
        panel.SetActive(true);
    }  
    public void OnPvP()
    {
        mode = GameMode.PvP;
        panel.SetActive(true);
    }
    public void OnTest()
    {
        mode = GameMode.Test;
        panel.SetActive(true);
    }
    public void OnHostButton()
    {
        isHost = true;
        hostMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void OnClientButton()
    {
        isHost = false;
        clientMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
