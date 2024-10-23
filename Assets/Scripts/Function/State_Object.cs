using UnityEngine;

public class State_Object : MonoBehaviour
{
    public GameObject Object;
    public void Set_State_Doll()
    {
            Debug.Log("Set_State_Doll");
            Object.SetActive(!Object.activeSelf);
            Fungus_Sp fungusSp_ = GetComponent<Fungus_Sp>();
            fungusSp_.ChangeState_D();
    }
}
