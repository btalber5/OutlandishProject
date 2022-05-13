using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueResponseEvent))]

public class DialogueResponseEventsEditor : Editor
{
    public override void OnInspectorGUI() {


        DrawDefaultInspector();
        DialogueResponseEvent responseEvents = (DialogueResponseEvent)target;

        if (GUILayout.Button("Refresh")) {

            responseEvents.OnValidate();
        
        }
    
    }
}
