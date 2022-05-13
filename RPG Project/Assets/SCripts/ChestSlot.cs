using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{
    public Text itemName;
    Item item;
    public Button removeButton;
    ChestScript scriptLoc;
    
    public void AddItem(Item newItem)
    {
        item = newItem;
        itemName = GetComponentInChildren<Text>();

        
        itemName.text = item.name;

    }


    public void ClearSlot()
    {

        item = null;
        itemName = GetComponentInChildren<Text>();
        itemName.text = "";



    }

    public void OnRemoveButton()
    {

        GetComponentInParent<ChestScript>().Remove(item);


    }

    

    public void UseItem()
    {
        scriptLoc = GetComponentInParent<ChestScript>();
        if (item != null)
        {
            item.canEquip = false;
            item.Use();
            item.TransferToInventory(scriptLoc);
        }

    }
}
