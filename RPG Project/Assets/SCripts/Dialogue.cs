using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class Dialogue : ScriptableObject
{

    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;

    public string[] Dialoguee => dialogue;

    public bool HasResponses => responses != null && responses.Length > 0;
    public Response[] Responses => responses;
    
}
