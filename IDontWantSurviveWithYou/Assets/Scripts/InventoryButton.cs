using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryButton : MonoBehaviour
{

    
    public Item food;
    public Text number;
    public GameObject food1; 
    public GameObject food2; 
    public GameObject food3;
    public Button Button; 
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
    }




}
