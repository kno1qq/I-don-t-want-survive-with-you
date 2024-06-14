using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI�ե�")]
    public Text textLabel;
    public Image faceImage;

    [Header("�奻���")]
    public TextAsset[] textFile = new TextAsset[2];
    public int fileIndex = 0;
    public int index = 0;
    public float textSpeed;

    [Header("�Y��")]
    public Sprite face01;
    public Sprite face02;
    public Sprite face03;
    public Sprite face04;
    public Sprite face05;
    public Sprite face06;

    List<string> textList = new List<string>();
    bool textFinished; //�O�_�������r
    bool cancelTyping; //�������r

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
        if(Input.GetKeyDown(KeyCode.R) && index == textList.Count) //��ܵ���
        {
            gameObject.SetActive(false);
            GameManager.instance.isTalk = false;
            //GameManager.instance.dialogPart += 1;
            //Debug.Log($"dialogPart = {GameManager.instance.dialogPart}");
            index = 0;
            return;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(textFinished && !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!textFinished) //�S�����b���r
            {
                cancelTyping = !cancelTyping;
            }
        }
    }
    //Ū���奻�ɮ�
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
    //��ܹ��UI
    IEnumerator SetTextUI()
    {
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
            case "F\r":
                faceImage.sprite = face06;
                index++;
                break;
        }
        /*
        for (int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];

            yield return new WaitForSeconds(textSpeed);
        }
        */
        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }
}