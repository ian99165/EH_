using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Fungus;

public class NPCInteraction : MonoBehaviour
{
    public bool canInteract;
    public string Name = "Name";
    public int _talk;
    
    public Flowchart flowchart;

    public void Interact_NPC()
    {
        if (!canInteract) return;
        switch (Name)
        {
            case "Doll" :
                Debug.Log("Doll");
                Doll();
                break;
            default:
                Debug.Log("No thing");
                break;
        }
    }

    private void Doll()
    {
        switch (_talk)
        {
            case 0:
                flowchart.ExecuteBlock("Doll_I_I");
                break;
            case 1:
                
                break;
            case 2:
                
                break;
            default:
                Debug.Log("無互動事件");
                break;
        }
    }

    public void VarAdd()
    {
        _talk++;
    }
}