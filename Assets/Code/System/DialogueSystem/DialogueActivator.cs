using System;
using UnityEngine;

/*
    Dialogue Activator should be mounted on the Dialoguer.
    DialogueData is needed in the inspector.
*/

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData dialogueData;
    public Action onDialogueStarted;
    public bool isDialogueActive = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.TryGetComponent(out PlayerInteract playerInteract))
        {
            playerInteract.interactable = this;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerInteract playerInteract))
        {
            if(playerInteract.interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                playerInteract.interactable = null;
            }
        }
    }

    public void Interact(PlayerInteract player)
    {
        if(isDialogueActive)
        {
            PlayerManager.instance.dialogueUI.ShowDialogue(dialogueData);
            
        }
        onDialogueStarted?.Invoke();
    }
}
