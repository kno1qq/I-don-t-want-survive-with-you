using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI組件")]
    public Text textLabel;
    public Image faceImage;

    [Header("文本文件")]
    public TextAsset[] textFile = new TextAsset[2];
    public int fileIndex = 0;
    public int index = 0;
    public float textSpeed;

    [Header("頭像")]
    public Sprite face01;
    public Sprite face02;
    public Sprite face03;
    public Sprite face04;
    public Sprite face05;

    List<string> textList = new List<string>();
    bool textFinished;

    public GameObject player;

    void Awake()
    {
        
    }
    private void OnEnable()
    {
        fileIndex = GameManager.instance.dialogPart;
        GetTextFormFile(textFile[fileIndex]);
        textFinished = true;
        StartCoroutine(SetTextUI());
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && index == textList.Count)
        {
            gameObject.SetActive(false);
            player.GetComponent<Player>().isEnabledWalk = true;
            GameManager.instance.dialogPart += 1;
            Debug.Log($"dialogPart = {GameManager.instance.dialogPart}");
            index = 0;
            return;
        }
        if(Input.GetKeyDown(KeyCode.R) && textFinished)
        {
            StartCoroutine(SetTextUI());
        }
    }
    //讀取文本檔案
    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineDate = file.text.Split('\n');
        foreach(var line in lineDate)
        {
            textList.Add(line);
        }
    }
    IEnumerator SetTextUI()
    {
        player.GetComponent<Player>().isEnabledWalk = false;
        textFinished = false;
        textLabel.text = "";

        switch (textList[index])
        {
            case "A\r":
                faceImage.sprite = face01;
                index++;
                break;
            case "B\r":
                faceImage.sprite = face02;
                index++;
                break;
            case "C\r":
                faceImage.sprite = face03;
                index++;
                break;
            case "D\r":
                faceImage.sprite = face04;
                index++;
                break;
            case "E\r":
                faceImage.sprite = face05;
                index++;
                break;
        }

        for (int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];

            yield return new WaitForSeconds(textSpeed);
        }
        textFinished = true;
        index++;
    }
}
