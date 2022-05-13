using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {

            Debug.LogWarning("More than one instance of Inventory found!");
        }

        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();
    public int space = 20;
    public int maxWeight = 50;
    public int currentWeight = 0;

    public void Add(Item item) {

        

        if (!item.isDefaultItem) {


            item.canEquip = true;
            items.Add(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            currentWeight += item.itemWeight;
            if (currentWeight > maxWeight)
            {

                Debug.Log("Encumbered");

            }
        }
    
    
    }

    public void Remove(Item item) {

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        items.Remove(item);
        
        
        currentWeight -= item.itemWeight;

    }
}
