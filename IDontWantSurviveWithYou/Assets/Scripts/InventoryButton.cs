using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{

    
    public Item food;
    public Text number;
    public GameObject food1; 
    public GameObject food2; 
    public GameObject food3;
    public Button Button;
    public GameObject currentUI; // 當前的 UI 面板
    public GameObject newUI; // 新的 UI 面板
    private int destroyCount = 0;


    void Start()
    {
        number.text = food.itemHeld.ToString();
    }

    public void DecreaseItem()
    {
        
        if (food != null && food.itemHeld > 0)
        {

            food.itemHeld -= 1;
            number.text = food.itemHeld.ToString();
            destroyCount++;
            switch (destroyCount)
            {
                case 1:
                    if (food1 != null) Destroy(food1);
                    break;
                case 2:
                    if (food2 != null) Destroy(food2);
                    break;
                case 3:
                    if (food3 != null) Destroy(food3);
                    break;
                default:                   
                    break;
            }
            if (destroyCount >= 3)
            {
                Button.interactable = false;               
            }
            print("A");           
        }
        
        if (food.itemHeld == 0 && destroyCount < 4)
        {
            if (currentUI != null) currentUI.SetActive(false);
            if (newUI != null) newUI.SetActive(true);
        }
    }
}
