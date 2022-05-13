
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public float radius;

    public Transform interactionTransform;
    bool isFocused = false;
    Transform player;
    internal bool hasInteracted = false;

    public virtual void Interact() {

        //method meeant to be ovverwritten for multiple interactions
        //Debug.Log("Interacting " + transform.name);
        
    
    }

    public void OnFocused(Transform playerTransform) {

        isFocused = true;
        player = playerTransform;
        hasInteracted = false;
    
    }

    public void OnDeFocused() {

        isFocused = false;
        player = null;
        hasInteracted = false;
    
    }


    private void Update()
    {
        if (isFocused && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);

            if (distance <= radius)
            {

                Interact();
                hasInteracted = true;


            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null) {

            interactionTransform = transform;
        
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position,radius);
    }

}
