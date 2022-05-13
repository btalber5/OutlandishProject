using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class Teleport : Interactable
{
    Transform location;
    
    bool clicked;
    public override void Interact()
    {
        base.Interact();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1 );


    }

}
