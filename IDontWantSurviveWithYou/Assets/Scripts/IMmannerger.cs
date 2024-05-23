using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IMmannerger : MonoBehaviour
{
    static IMmannerger instance;
    public Inventory mybag;
    public GameObject slotGrid;
    public slot slotPrefab;
    public Text itemInfromation;
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItem();
    }
    public static void CreateNewItem(Item item)
    {
        slot newItem = Instantiate(instance.slotPrefab,instance.slotGrid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite=item.itemImage;
        newItem.slotNum.text=item.itemHeld.ToString();
    }
    public static void RefreshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if(instance.slotGrid.transform.childCount==0)
            break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < instance.mybag.itemList.Count; i++)
        {
            CreateNewItem(instance.mybag.itemList[i]);

        }
    }
}
