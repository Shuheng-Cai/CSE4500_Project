using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerInteract playerInteract))
        {
            playerInteract.interactable = this;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerInteract playerInteract))
        {
            if (playerInteract.interactable is ChestInteractable chestInteractable && chestInteractable == this)
            {
                playerInteract.interactable = null;
            }
        }
    }

    public void Interact(PlayerInteract player)
    {
        DialogueUI dialogueUI = PlayerManager.instance.dialogueUI;
        if (dialogueUI == null)
        {
            Debug.LogWarning("No DialogueUI found. Make sure the Dialogue prefab is in the scene.");
            return;
        }

        string[] dialogues = new string[]
        {
            "Looks like you have " + GoldManager.instance.currentGold + " coins collected!",
            "Return to the store area by following the stone path that leads up and away from this campsite.",
            "There you'll be able to put your hard-earned coins to use."
        };
        dialogueUI.ShowDialogue(dialogues);
    }
}
