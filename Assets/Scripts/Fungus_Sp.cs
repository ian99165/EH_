using System;
using System.Collections;
using System.Collections.Generic;
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
        /*物件狀態控制 程式呼叫方式
            Fungus_Sp fungusSp_ = GetComponent<Fungus_Sp>();
            fungusSp_.ChangeState_D();
        */
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
