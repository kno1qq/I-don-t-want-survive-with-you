using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWork : MonoBehaviour
{
    public Item thisItem;
    public Inventory playerinventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }

        
    }
    public void AddNewItem()
    {
        if(!playerinventory.itemList.Contains(thisItem)) 
        {
            playerinventory.itemList.Add(thisItem);
            //IMmannerger.CreateNewItem(thisItem);
        }
        else
        {
            thisItem.itemHeld += 1;
        }
        IMmannerger.RefreshItem();
    }
}
