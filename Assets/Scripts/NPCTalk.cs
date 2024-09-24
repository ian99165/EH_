using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    public string NPCName;
    public string Talk;


    public void TalkNPC()
    {
        if (NPCName == "NPC_1")
        {
            if (Talk == "1")
            {
                Debug.Log("說說1");
            }
        }

        if (NPCName == "NPC_2")
        {
            
        }
    }
}
