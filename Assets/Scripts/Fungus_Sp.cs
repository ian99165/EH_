using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class Fungus_Sp : MonoBehaviour
{
    public string PlayerName = "Player";
    
    public bool isDollTalk;
    public bool isLaydTalk_I;
    public bool isLaydTalk_II;
    
    public Flowchart flowchart;
    public void Talking_Doll()
    {
        flowchart.ExecuteBlock("Doll_I_I");
    }
    public void Talking_L_I()
    {
        flowchart.ExecuteBlock("Layd_I_I");
    }
    public void Talking_L_II()
    { 
        flowchart.ExecuteBlock("Layd_I_I");
    }
}
