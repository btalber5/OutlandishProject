using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public Transform itemsParent;
    InventorySlot[] slots;
    public Text itemName;
    Canvas myCanvas;
    Button childButton;
    public bool toggleCanvas;

    // Start is called before the first frame update
    void Start()
    {
        myCanvas = GetComponent<Canvas>();
        toggleCanvas = false;
        inventory = Inventory.instance;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventory.onItemChangedCallback += UpdateUI;

        childButton = GetComponentInChildren<Button>();

        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleCanvas == false)
        {

            HideUI();
        }
        else showUI();
    }

    void UpdateUI() {
        
        
        

        Debug.Log(inventory.items.Count);
        for (int i = 0; i < slots.Length; i++) {
            
            if (i < inventory.items.Count)
            {
                
                slots[i].AddItem(inventory.items[i]);
            }

            else { 
            
                slots[i].ClearSlot();

            }

        
        }

        
    
    }


    public void HideUI() {

        
        myCanvas.enabled = false;
    
    }

    public void showUI() {

        
        myCanvas.enabled = true;

    }
}
