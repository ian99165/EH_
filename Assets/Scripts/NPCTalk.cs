using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    public string NPCName;


    public void TalkNPC()
    {
        Fungus_Sp fungusSp_ = GetComponent<Fungus_Sp>();
        if (NPCName == "Doll")
        {
            fungusSp_.Talking_Doll();
        }

        if (NPCName == "Layd")
        {
            
        }
    }
}
