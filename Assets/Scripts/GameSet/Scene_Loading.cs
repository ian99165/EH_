using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Loading : MonoBehaviour
{
    private int _scene = 0;

    public void LoadScene()
    {
        switch (_scene)
        {
            case 1:
                //載入S1
            break;
            case 2:
                //載入S2
            break;
            case 3:
                //載入S3
            break;
            default:
                Debug.Log("無法載入場景");
        }
    }

    public Void SetScene()
    {
        _scene++;
    }
}
