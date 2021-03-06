using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;
    private DialogueUI dialogueUI;
    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private ResponseEvent[] responseEvents;

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
        responseBox.gameObject.SetActive(false);
        
    }
    public void ShowResponses(Response[] responses) {

        float boxHeight = 0;
        for(int i = 0; i < responses.Length; i++) {

            Response response = responses[i];
            int responseIndex = i;

            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));
            tempResponseButtons.Add(responseButton);
            boxHeight += responseButtonTemplate.sizeDelta.y;

        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, boxHeight);
        responseBox.gameObject.SetActive(true);

    }

    private void OnPickedResponse(Response response,int responseIndex) { 

        
        responseBox.gameObject.SetActive(false);
        foreach (GameObject button in tempResponseButtons) {


            Destroy(button);
            
        }
        tempResponseButtons.Clear();

        if (responseEvents != null && responseIndex <= responseEvents.Length) {

            responseEvents[responseIndex].OnPickedResponse.Invoke();
        
        
        }

        responseEvents = null;
        
        if (response.Dialogue)
        {
            dialogueUI.ShowDialogue(response.Dialogue);
            
        }
        else {
            dialogueUI.CloseDialogueBox();
        }



    }

    public void AddResponseEvent(ResponseEvent[] responseEvents) {

        this.responseEvents = responseEvents;
    
    }


}
