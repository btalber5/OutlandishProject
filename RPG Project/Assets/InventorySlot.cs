using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Text itemName;
    Item item;
    public Button removeButton;


    public void AddItem(Item newItem) {
        item = newItem;
        itemName = GetComponentInChildren<Text>();

        itemName.text = item.name;

    }


    public void ClearSlot(){

        item = null;
        itemName = GetComponentInChildren<Text>();
        itemName.text = "";

        
    
    }

    public void OnRemoveButton() {

        Inventory.instance.Remove(item);
    
    
    }

    public void UseItem() { 
    
        if(item != null)
        {
            item.Use();

        }
    
    }

}
