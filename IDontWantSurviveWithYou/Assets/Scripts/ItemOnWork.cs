using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemonwork : MonoBehaviour
{
    public Item thisItem;
    public Inventory playerinventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (!GameManager.instance.grid.activeSelf)
            {
                GameManager.instance.openGrid();
            }
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
