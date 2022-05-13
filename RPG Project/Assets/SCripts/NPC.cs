using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    private PlayerManager playerManager;
    [SerializeField] private Dialogue dialogueObject;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] List<Dialogue> dialogueList = new List<Dialogue>();
    Animator animator;


    private void Start()
    {
        
        dialogueUI.gameObject.SetActive(false);
        animator = GetComponentInChildren<Animator>();
    }

    public override void Interact()
    {
        base.Interact();
        dialogueUI.gameObject.SetActive(true);
        
        if (dialogueUI.gameObject.active)
        {
            animator.SetBool("canTalk",true);
            foreach (DialogueResponseEvent responseEvents in GetComponents<DialogueResponseEvent>()) {


                if (responseEvents.DialogueObject == dialogueObject) {

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

    public void UpdateAndDisplay(Dialogue dialogueIn) {

        this.dialogueObject = dialogueIn;
        dialogueUI.ShowDialogue(this.dialogueObject);

    }
    

    
}
