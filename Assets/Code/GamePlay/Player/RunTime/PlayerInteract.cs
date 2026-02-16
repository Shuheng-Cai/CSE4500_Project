using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable interactable {get; set;}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(1);
            interactable?.Interact(this);  
        }
    }
}
