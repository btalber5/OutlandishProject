using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfer : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();

        TransferToInventroy(item);
    }

    public void TransferToInventroy(Item item)
    {

        Debug.Log("Move to inventory");
        Inventory.instance.Add(item);
        ChestScript.chestInstance.Remove(item);

    }
}
