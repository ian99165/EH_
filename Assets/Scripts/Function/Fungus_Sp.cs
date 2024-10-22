using UnityEngine;
using Fungus;


public class Fungus_Sp : MonoBehaviour
{
    public string PlayerName = "Player";
    
    public bool isDollTalk;
    public bool isLaydTalk_I;
    public bool isLaydTalk_II;
    //狀態
    public bool Doll_State = false;

    public Flowchart flowchart;
    public void Talking_Doll()
    {
        if (Doll_State)
        {
            flowchart.ExecuteBlock("Doll_I_I");
        }
    }
    public void Talking_L_I()
    {
        flowchart.ExecuteBlock("Layd_I_I");
    }
    public void Talking_L_II()
    { 
        flowchart.ExecuteBlock("Layd_I_I");
    }
    
    //狀態控制
    public void ChangeState_D()
    {
        Doll_State = true;
    }
}
