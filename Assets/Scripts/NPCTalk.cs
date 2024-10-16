using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    public string NPCName;
    public int Talk;


    public void TalkNPC()
    {
        CanvaUI ui_ = GetComponent<CanvaUI>();
        Fungus_Sp fungusSp_ = GetComponent<Fungus_Sp>();
        if (NPCName == "Doll")
        {
            ui_.OpenCanva();
            fungusSp_.Talking_D();
        }

        if (NPCName == "NPC_2")
        {
            
        }
    }
}
