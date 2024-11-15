using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public string Name = "Name";

    public void Interact_Item()
    {
        switch (Name)
        {
            case "Key":
                Debug.Log("123");
                break;
            default:
                Debug.Log("無互動事件");
                break;
        }
    }

    public void VarAdd()
    {
        
    }
}