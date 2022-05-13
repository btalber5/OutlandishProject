using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{


    public EquipSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public int armour;
    public int damage;

    public override void Use()
    {
        base.Use();

        //equip
        if (canEquip == true) {
            EquipmentManager.instance.Equip(this);
        }

            
        
        
        // remove from inventory
        
        removeFromInventory();
    }


}

public enum EquipSlot { 
    
    Head, Chest, Legs, Feet, Hands, MainHand, OffHand


}

public enum EquipmentMeshRegion { 


        Legs, Arms, Torso // corresponds to body blend shapes

}
