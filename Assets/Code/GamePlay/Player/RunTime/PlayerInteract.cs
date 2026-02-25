using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This is PlayerInteract. Implement IInteractable interface and invoke it.
    Interactable is set in DialogueActivate.
*/

public class PlayerInteract : MonoBehaviour
{
    public IInteractable interactable {get; set;}

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(1);
            interactable?.Interact(this);  
        }
    }
}
