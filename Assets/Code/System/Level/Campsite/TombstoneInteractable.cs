using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombstoneInteractable : MonoBehaviour, IInteractable
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
            if (playerInteract.interactable is TombstoneInteractable tombstoneInteractable && tombstoneInteractable == this)
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
            Debug.LogWarning("No DialogueUI was found. Make sure the Dialogue prefab is in the scene.");
            return;
        }

        // How many enemies and of what type have you defeated
        string[] dialogues = new string[]
        {
            "You have defeated " + GoldManager.instance.currentGold + " total enemies!",
            "You have fought bravely. Good luck in the next level."
        };
        dialogueUI.ShowDialogue(dialogues);
    }
}
