using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Interactable
{
    private PlayerManager playerManager;
    [SerializeField] private Dialogue dialogueObject;
    [SerializeField] private DialogueUI dialogueUI;

    private void Start()
    {

        dialogueUI.gameObject.SetActive(false);

    }

    public override void Interact()
    {
        base.Interact();
        dialogueUI.gameObject.SetActive(true);
        if (dialogueUI.gameObject.active)
        {
            foreach (DialogueResponseEvent responseEvents in GetComponents<DialogueResponseEvent>())
            {


                if (responseEvents.DialogueObject == dialogueObject)
                {

                    dialogueUI.AddResponseEvent(responseEvents.Events);
                    break;
                }
            }
            dialogueUI.ShowDialogue(dialogueObject);
        }


    }

    public void UpdateDialogueObject(Dialogue dialogueObject)
    {
        this.dialogueObject = dialogueObject;


    }

    public void UpdateAndDisplay(Dialogue dialogueIn)
    {

        this.dialogueObject = dialogueIn;
        dialogueUI.ShowDialogue(this.dialogueObject);

    }
}
