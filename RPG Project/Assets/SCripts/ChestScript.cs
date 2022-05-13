using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : Interactable
{

    #region Singleton
    public static ChestScript chestInstance;

    private void Awake()
    {
        if (chestInstance != null)
        {

            
        }

        chestInstance = this;
    }
    #endregion


    Transform target;


    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();
    public int space = 20;
    bool isShowing = false;
    bool canOpen;

    ChestInventoryUI UIManager;
    Interactable parent;
    public override void Interact()
    {
        base.Interact();


        if (canOpen == true)
        {
            OpenStorage();
        }

        else { CloseStorage(); }


    }



    private void Start()
    {
        canOpen = false;
        target = PlayerManager.instance.Player.transform;
        UIManager = GetComponentInChildren<ChestInventoryUI>();
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

    }

    private void LateUpdate()
    {
        float distance = Vector3.Distance(target.position, this.transform.position);

        if (distance < this.radius)
        {
            canOpen = true;

        }

        else canOpen = false;
    }




    public void Remove(Item item)
    {

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        items.Remove(item);

    }

    void OpenStorage()
    {


        UIManager.toggleCanvas = !UIManager.toggleCanvas;





    }

    void CloseStorage()
    {


        UIManager.toggleCanvas = !UIManager.toggleCanvas;




    }
}
