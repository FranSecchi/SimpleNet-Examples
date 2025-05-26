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
    public GameObject HostBtn;
    public GameObject ClientBtn;
    public GameObject PongBtn;
    public GameObject PvPBtn;
    public GameObject TestBtn;
    public GameObject BackBtn;
    public GameObject hostMenu;
    public GameObject clientMenu;
    public static GameMode mode = GameMode.PvP;
    public static bool isHost = false;

    public void OnPong()
    {
        mode = GameMode.Pong;
        HostClient(true);
    }  
    public void OnPvP()
    {
        mode = GameMode.PvP;
        HostClient(true);
    }
    public void OnTest()
    {
        mode = GameMode.Test;
        HostClient(true);
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

    public void HostClient(bool activate)
    {
        HostBtn.SetActive(activate);
        ClientBtn.SetActive(activate);
        BackBtn.SetActive(activate);
        
        PongBtn.SetActive(!activate);
        PvPBtn.SetActive(!activate);
        TestBtn.SetActive(!activate);
    }

}
