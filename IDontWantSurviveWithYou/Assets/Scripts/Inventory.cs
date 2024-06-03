using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]


public class Inventory : ScriptableObject
{
    
    public List<Item>itemList=new List<Item>();

    public Item GetItem(string itemName)
    {
        return itemList.Find(item => item.itemName == itemName);
    }


}
