using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response 
{

    [SerializeField] private string responseText;
    [SerializeField] public Dialogue dialogueObject;
    [SerializeField] internal bool hasDialogue;

    public string ResponseText => responseText;

    public Dialogue Dialogue => dialogueObject;
    
}
