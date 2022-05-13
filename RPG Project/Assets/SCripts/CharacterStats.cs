using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat health;
    public Stat damage;
    public Stat armor;
    public Stat magicDamage;
    public Stat magicResist;
    public Stat coldResist;
    public Stat coldDamage;
    public Stat fireResist;
    public Stat fireDamage;
    public Stat attackSpeed;

    public int currentHealth;

    

    private void Awake()
    {
        currentHealth = health.getValue();

    }


    private void Update()
    {
        
    }

    public void TakeDamage(int damage) {

        damage -= armor.getValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        Debug.Log(transform.name + "takes " + damage + " damage.");
        if (currentHealth <= 0) {


            Die();
        
        
        }
    }



    public void setDamage(int damageIn) {

        this.damage.setValue(damageIn);
    
    }

    public virtual void Die() {


        Debug.Log(transform.name + " Died");
    
    
    }
}
