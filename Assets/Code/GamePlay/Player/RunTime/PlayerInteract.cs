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
    private bool isDialogue = false;

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isDialogue)
        {
            Debug.Log(1);
            isDialogue = true;
            interactable?.Interact(this);  
        }

        if(interactable == null)
        {
            isDialogue = false;
        }
    }
}
