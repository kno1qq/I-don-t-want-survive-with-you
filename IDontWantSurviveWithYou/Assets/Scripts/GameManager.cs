using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("UI")]
    public int dialogPart = 0;
    public GameObject tipPanel;
    public Text tipText;
    public GameObject screenPanel;
    public Text screenPanelText;
    public GameObject grid;
    public GameObject dialogPanel;
    public GameObject specialObject;
    public GameObject endUIPanel; // �s�W������UI���O

    public GameObject player;

    public bool isTalk = false;
    public int GameState;

    public static GameManager Instance;

    private List<NPCInteraction> npcList = new List<NPCInteraction>();
    private int npcTalkedCount = 0;

    private bool isButton3FromWomenrightClicked = false;
    private bool isButton3FromM3rightClicked = false;
    private bool isButton3FromOldrightClicked = false;
    private bool isButton3FromFoodrightClicked = false;
    [Header("����")]
    public GameObject audioManagerPrefab;//���ĺ޲z017
    public AudioManager audioManager;//���ĺ޲z017
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        audioManager = Instantiate(audioManagerPrefab).GetComponent<AudioManager>();//���oaudioManagerPrefab017
    }
    public void OpenTipPanel(string text)
    {
        StartCoroutine(showTip(text));
    }
    //�Ĥ@���ĤG���ĤT��
    public void openScreenPanel(string text)
    {
        screenPanelText.text = text;
        screenPanel.SetActive(true);
    }
    public void closeScreenPanel()
    {
        screenPanel.SetActive(false);
    }
    public void openGrid()
    {
        grid.SetActive(true);
    }
    public void closeGrid()
    {
        grid.SetActive(false);
    }
    public void openDialog(int part)
    {
        isTalk = true;
        dialogPart = part;
        dialogPanel.SetActive(true);
    }
    public IEnumerator showTip(string text)
    {
        tipText.text = text;
        tipPanel.SetActive(true);
        print("openTip");
        yield return new WaitForSeconds(3);
        tipPanel.SetActive(false);
        print("Tip");
    }
    public void RegisterNPC(NPCInteraction npc)
    {
        npcList.Add(npc);
    }

    public void NPCTalkedTo(NPCInteraction npc)
    {
        npcTalkedCount++;
        if (npcTalkedCount >= npcList.Count)
        {
            GameManager.instance.openDialog(4);
        }
    }

    public void Button3Clicked(string source)
    {
        switch (source)
        {
            case "Womenright":
                isButton3FromWomenrightClicked = true;
                break;
            case "M3right":
                isButton3FromM3rightClicked = true;
                break;
            case "Oldright":
                isButton3FromOldrightClicked = true;
                break;
            case "Foodright":
                isButton3FromFoodrightClicked = true;
                break;
        }

        if (isButton3FromWomenrightClicked && isButton3FromM3rightClicked && isButton3FromOldrightClicked && isButton3FromFoodrightClicked)
        {

            endUIPanel.SetActive(true);
            specialObject.SetActive(true);
        }
    }
    public void OnPlayerEnterBossShowBox()//�������a�O�_�i�JBoss�ͦ���
    {
        instance.openDialog(0);
        Camerashake.Instance.ShakeCamera(5f, .3f);
        Vector3 spawnPosition = new Vector3(-16.3f, -2.8f, 0);
        GameObject BigBoss = Resources.Load<GameObject>("prefabs/BigBoss");
        Instantiate(BigBoss, spawnPosition, Quaternion.identity);
    }

}