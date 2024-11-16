using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public string Name = "Name";
    private bool _check = false;

    public void Interact_Item()
    {
        _check = true;
    }
    
    public void Interact_Item_Exit()
    {
        if(_check)
        {
            _check = false;
        }
    }
}