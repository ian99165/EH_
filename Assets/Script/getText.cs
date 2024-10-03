using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class getText : MonoBehaviour
{
    public TextAsset TXTfile;
    public List<string> listTxt = new List<string>();
    public int indx;
    public TextMeshProUGUI txt;
    public float textSpeed;
    public bool txtFinished;

    void readFile(TextAsset file)
    {
        listTxt.Clear();
        indx = 0;
        var linData = file.text.Split('\n');

        foreach ( var line in linData)
        {
            listTxt.Add(line);
        }
        
    }
    private void Awake()
    {
        readFile(TXTfile);
    }

    IEnumerator  SetUI()
    {
        txtFinished = false;
        //�N�C��M��
        txt.text = "";

        for (int i = 0; i < listTxt[indx].Length; i++)
        {
            txt.text += listTxt[indx][i];
            yield return new WaitForSeconds(textSpeed);
        }
        txtFinished = true;
        indx++;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetUI());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && txtFinished)
        {
            StartCoroutine(SetUI());
        }
    }
}
