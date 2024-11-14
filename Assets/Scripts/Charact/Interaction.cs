using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public string Name = "Name";
    public int _doll;
    private int _lady;
    private int _doctor;
    private int _kimera;

    public void Interact_Item()
    {
        switch (Name)
        {
            case "123":
                Debug.Log("123");
                break;
            default:
                Debug.Log("無互動事件");
                break;
        }
    }

    public void Interact_NPC()
    {
        switch (Name)
        {
            case "Doll" :
                Debug.Log("Doll");
                break;
            default:
                Debug.Log("No NPC");
                break;
        }
    }

    private void Doll()
    {
        switch (_doll)
        {
            case 0:
                
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
        switch (Name)
        {
            case "Doll":
                _doll++;
                break;
            case "Lady":
                
                break;
            default:
                Debug.Log("無互動事件");
                break;
        }
    }
}