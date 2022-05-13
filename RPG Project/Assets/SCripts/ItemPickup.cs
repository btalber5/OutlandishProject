using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : Interactable
{

    public Item item;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp() {

        
        Debug.Log("Pick up " + item.name);
        Inventory.instance.Add(item);
        Destroy(gameObject);
        
    
    }

}
