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
        if (NPCName == "Doll")
        {
            Debug.Log("11");
            ui_.OpenCanva();
        }

        if (NPCName == "NPC_2")
        {
            
        }
    }
}
