using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWater : MonoBehaviour
{


    public Item water;
    public Text number;
    public GameObject water1;
    public GameObject water2;
    public GameObject water3;
    public GameObject water4;
    public Button Button;
    private int destroyCount = 0;

    void Start()
    {
        number.text = water.itemHeld.ToString();
    }

    public void DecreaseItem()
    {

        if (water != null && water.itemHeld > 0)
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
                default:                   
                    break;
            }
            if (destroyCount >= 4)
            {
                Button.interactable = false;
            }
            print("B");
        }

    }




}
