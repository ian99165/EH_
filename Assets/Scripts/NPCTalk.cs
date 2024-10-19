using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class NPCTalk : MonoBehaviour
{
    public string NPCName;

    public void TalkNPC()
    {
        CanvaUI ui_ = GetComponent<CanvaUI>();
        Fungus_Sp fungusSp_ = GetComponent<Fungus_Sp>();
        if (NPCName == "Doll")
        {
            ui_.MsOFF();
            fungusSp_.Talking_Doll();
        }

        if (NPCName == "NPC_2")
        {
            
        }
    }
    
    public void EndTalking()
    {
        CanvaUI ui_ = GetComponent<CanvaUI>();

        //isTalking = false;
        ui_.MsON();
    }
}
