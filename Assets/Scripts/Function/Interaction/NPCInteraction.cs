using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteraction : MonoBehaviour
{
    public string Name = "Name";
    public int _talk;

    public void Interact_NPC()
    {
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
                VarAdd();
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