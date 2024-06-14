using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossWater : MonoBehaviour
{


    public Item water;
    public Text number;
    public GameObject water1;
    public GameObject water2;
    public GameObject water3;
    public GameObject water4;
    public GameObject water5;
    public GameObject water6;
    public GameObject water7;
    public GameObject water8;
    public Button Button;
    public GameObject currentUI; // 當前的 UI 面板
    public GameObject newUI; // 新的 UI 面板
    private int destroyCount = 0;

    void Start()
    {
        number.text = water.itemHeld.ToString();
    }

    public void DecreaseItem()
    {

        if (water != null)
        {
            water.itemHeld -= 1;
            number.text = water.itemHeld.ToString();
            destroyCount++;
            switch (destroyCount)
            {
                case 1:
                    if (water1 != null) Destroy(water1);
                    break;
                case 2:
                    if (water2 != null) Destroy(water2);
                    break;
                case 3:
                    if (water3 != null) Destroy(water3);
                    break;
                case 4:
                    if (water4 != null) Destroy(water4);
                    break;
                case 5:
                    if (water5 != null) Destroy(water5);
                    break;
                case 6:
                    if (water6 != null) Destroy(water6);
                    break;
                case 7:
                    if (water7 != null) Destroy(water7);
                    break;
                case 8:
                    if (water8 != null) Destroy(water8);
                    break;
                default:
                    break;
            }
            if (destroyCount >= 8)
            {
                Button.interactable = false;
            }
            print("B");
        }


        if (water.itemHeld == 0 && destroyCount < 8)
        {
            if (currentUI != null) currentUI.SetActive(false);
            if (newUI != null) newUI.SetActive(true);
        }

    }

}
