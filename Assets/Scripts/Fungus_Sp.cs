using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class Fungus_Sp : MonoBehaviour
{
    public string PlayerName = "Player";
    private bool hasTalk = false;
    
    private bool isDollTalk;
    private bool isLaydTalk_I;
    private bool isLaydTalk_II;
    
    public Flowchart flowchart;

    private void Start()
    {
        isDollTalk = true;
    }

    private void Update()
    {
        //Talking();
    }

    private void Talking()
    {
        if(!hasTalk)
        {
            hasTalk = true;
            if (isDollTalk)
            {
                flowchart.ExecuteBlock("Doll_I_I");
            }
            
            if (isLaydTalk_I)
            {
                flowchart.ExecuteBlock("Layd_I_I");
            }
            
            if (isLaydTalk_II)
            {
                flowchart.ExecuteBlock("Layd_I_I");
            }
        }
    }
}
