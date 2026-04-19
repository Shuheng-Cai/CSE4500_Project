using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChestInteractable : MonoBehaviour, IInteractable {
    static readonly int IsOpenedHash = Animator.StringToHash("IsOpened");

    Animator animator;
    bool dialogActive;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerInteract playerInteract)) {
            playerInteract.interactable = this;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerInteract playerInteract)) {
            if (playerInteract.interactable is ChestInteractable chestInteractable && chestInteractable == this) {
                playerInteract.interactable = null;
            }
        }
    }

    public void Interact(PlayerInteract player) {
        if (dialogActive) return;

        DialogueUI dialogueUI = PlayerManager.instance.dialogueUI;
        if (dialogueUI == null) {
            Debug.LogWarning("No DialogueUI found. Make sure the Dialogue prefab is in the scene.");
            return;
        }

        string[] dialogues = new string[] {
            "Looks like you have " + GoldManager.instance.currentGold + " coins collected!",
            "Return to the store area by following the stone path that leads up and away from this campsite.",
            "There you'll be able to put your hard-earned coins to use."
        };

        animator.SetBool(IsOpenedHash, true);
        dialogActive = true;
        dialogueUI.ShowDialogue(dialogues);
        StartCoroutine(CloseWhenDialogueEnds(dialogueUI));
    }

    IEnumerator CloseWhenDialogueEnds(DialogueUI dialogueUI) {
        yield return null;
        yield return new WaitUntil(() => !dialogueUI.isOpen);
        animator.SetBool(IsOpenedHash, false);
        dialogActive = false;
    }
}
