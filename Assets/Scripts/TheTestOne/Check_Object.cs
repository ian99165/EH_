using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Object : MonoBehaviour
{

    public void Set_State()
    {
            Fungus_Sp fungusSp_ = GetComponent<Fungus_Sp>();
            fungusSp_.ChangeState_D();
    }
}
