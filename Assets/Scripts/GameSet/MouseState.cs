using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseState : MonoBehaviour
{
    private int _mouse_state = 0 ;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MouseMode_I()
    {
        _mouse_state = 0;
        Mode_state();
    }

    public void MouseMode_II()
    {
        _mouse_state = 1;
        Mode_state();
    }

    public void Mode_state()
    {
        if (_mouse_state == 0) //探索模式
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (_mouse_state == 1) //滑鼠模式
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
