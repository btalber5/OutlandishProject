using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {

            TakeDamage(30);

        }
    }

    void OnEquipmentChanged(Equipment newItem) {

        if (newItem != null)
        {
            armor.AddModifier(newItem.armour);
            damage.AddModifier(newItem.damage);
        }
        
    
    }

    public override void Die()
    {
        base.Die();
        //kill the player
        PlayerManager.instance.KillPlayer();
    }
}
