using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation : MonoBehaviour
{
    [Header("UI組件")]
    public Text textLabel;
    public Image faceImage;

    [Header("文字文件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("人像")]
    public Sprite faceA, faceB;

    bool textFinished;

    List<string> textList = new List<string>();
    // Start is called before the first frame update
    void Awake()
    {
        GetTextFormFile(textFile);

    }

    //啟用時直接開始第一句話
    private void OnEnable()
    {
        //textLabel.text = textList[index];
        //index++;
        textFinished = true;
        StartCoroutine(SetTextUI());
    }

    // Update is called once per frame
    void Update()
    {
        //判斷對話來到最後一行
        if (Input.GetKeyDown(KeyCode.Space) && index == textList.Count)
        {
            gameObject.SetActive(false);
            Destroy(this);
            //對話框序列歸零
            index = 0;
            return;
        }
        //按下空白鍵換行
        if (Input.GetKeyDown(KeyCode.Space) && textFinished)
        {
            //textLabel.text = textList[index];
            //index++;
            StartCoroutine(SetTextUI());
        }
    }
    public void changeTxt()
    {
        StartCoroutine(SetTextUI());
    }
    //獲得文字
    void GetTextFormFile(TextAsset file)
    {
        //對話框序列歸零
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        //將列表清空
        textLabel.text = "";
        Debug.Log(textList[index]);
        //頭像
        switch (textList[index])
        {
            case "小鬼 (NPC)：\r":
                faceImage.sprite = faceA;
                index++;
                break;
            case "安琪 ( 主角 )：\r":
                faceImage.sprite = faceB;
                index++;
                break;
            
        }

        //將字符累加進來
        for (int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];
            yield return new WaitForSeconds(textSpeed);
        }

        textFinished = true;
        index++;
    }
}
