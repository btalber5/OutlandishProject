using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField]  Dialogue testDialogue;
    private TypeWriter typeWriter;
    private ResponseHandler responseHandler;
    Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        typeWriter = GetComponent<TypeWriter>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(Dialogue dialogueObject) {
        if (dialogueBox.activeSelf == false)
        {
            dialogueBox.SetActive(true);
            StartCoroutine(StepThroughDialogue(dialogueObject));
        }
        else StartCoroutine(StepThroughDialogue(dialogueObject));
    
    }


    private IEnumerator StepThroughDialogue(Dialogue dialogueObject) {



        for (int i = 0; i < dialogueObject.Dialoguee.Length;i++) {

            string dialogue = dialogueObject.Dialoguee[i];
            yield return typeWriter.Run(dialogue, textLabel);
            

            if (i == dialogueObject.Dialoguee.Length - 1 && dialogueObject.HasResponses) break;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);

        }
        else
        {
            animator.SetBool("canTalk", false);
            CloseDialogueBox();
        }
       
    }

    internal void CloseDialogueBox() {

        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    
    }

    public void AddResponseEvent(ResponseEvent[] responseEvents) {

        responseHandler.AddResponseEvent(responseEvents);
    
    
    }

}
