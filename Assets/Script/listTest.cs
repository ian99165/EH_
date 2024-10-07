using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listTest : MonoBehaviour
{
    public List<string> txtList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        txtList.Add("洋娃娃:");
        txtList.Add("你好呀！");
        foreach (var line in txtList)
        {
            Debug.Log(line);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
