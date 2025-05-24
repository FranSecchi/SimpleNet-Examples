using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI player1;
    public TextMeshProUGUI player2;
    public TextMeshProUGUI player3;
    public TextMeshProUGUI player4;
    public Health h1;
    public Health h2;
    public Health h3;
    public Health h4;
    private static UI instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(player1.isActiveAndEnabled)
            instance.player1.text = "HP: " + h1.lifes;
        if(player2.isActiveAndEnabled)
            instance.player2.text = "HP: " + h2.lifes;
        if(player3.isActiveAndEnabled)
            instance.player3.text = "HP: " + h3.lifes;
        if(player4.isActiveAndEnabled)
            instance.player4.text = "HP: " + h4.lifes;
    }
}
