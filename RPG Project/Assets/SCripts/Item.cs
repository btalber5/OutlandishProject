using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon;
    public bool isDefaultItem = false;
    public int itemWeight;
    public bool canEquip;



    public virtual void Use() {


        Debug.Log("Using" + name);
    
    
    }

    public void removeFromInventory() {

        Inventory.instance.Remove(this);
        
    
    }

    public void TransferToInventory(ChestScript scriptLoc)
    {
        
        Debug.Log("Move to inventory");
        Inventory.instance.Add(this);
        scriptLoc.Remove(this);
        

    }






}
