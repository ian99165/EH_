using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    public string NPCName;
    public int Talk;


    public void TalkNPC()
    {
        Canva_UI ui_ = GetComponent<Canva_UI>();
        if (NPCName == "Doll")
        {
            Debug.Log("11");
            ui_.Canva();
        }

        if (NPCName == "NPC_2")
        {
            
        }
    }
}
