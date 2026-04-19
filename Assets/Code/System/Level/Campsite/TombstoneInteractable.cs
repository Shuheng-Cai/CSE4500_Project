using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TombstoneInteractable : MonoBehaviour, IInteractable {
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerInteract playerInteract)) {
            playerInteract.interactable = this;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerInteract playerInteract)) {
            if (playerInteract.interactable is TombstoneInteractable tombstoneInteractable && tombstoneInteractable == this) {
                playerInteract.interactable = null;
            }
        }
    }

    public void Interact(PlayerInteract player) {
        DialogueUI dialogueUI = PlayerManager.instance.dialogueUI;
        if (dialogueUI == null) {
            Debug.LogWarning("No DialogueUI was found. Make sure the Dialogue prefab is in the scene.");
            return;
        }

        EnemyKillManager km = EnemyKillManager.instance;
        if (km == null || km.TotalKills == 0) {
            dialogueUI.ShowDialogue(new string[] {
                "No enemies fell in your last battle.",
                "Sharpen your blade and return to this tombstone to pay your respects."
            });
            return;
        }

        StringBuilder breakdown = new StringBuilder();
        bool first = true;
        foreach (KeyValuePair<string, int> kv in km.Kills) {
            if (kv.Value <= 0) continue;
            if (!first) breakdown.Append(", ");
            breakdown.Append(kv.Key).Append(" \u00d7 ").Append(kv.Value);
            first = false;
        }

        string[] dialogues = new string[] {
            "You defeated " + km.TotalKills + " enemies in your last battle.",
            "The tally: " + breakdown.ToString() + ".",
            "You have fought bravely. Good luck in the next level."
        };
        dialogueUI.ShowDialogue(dialogues);
    }
}
