
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData dialogueData;

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
        player.DialogueUI.ShowDialogue(dialogueData);
    }
}
