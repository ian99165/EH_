using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Object : MonoBehaviour
{

    public void Set_State_Doll()
    {
            Debug.Log("Set_State_Doll");
            Fungus_Sp fungusSp_ = GetComponent<Fungus_Sp>();
            fungusSp_.ChangeState_D();
    }
}
