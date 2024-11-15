using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public string Name = "Name";
    private bool _check = false;

    public void Interact_Item()
    {
        //物件旋轉查看
        _check = true;
    }
    
    public void Interact_Item_Exit()
    {
        if(_check)
        {
            //關閉物件
        }
    }
}