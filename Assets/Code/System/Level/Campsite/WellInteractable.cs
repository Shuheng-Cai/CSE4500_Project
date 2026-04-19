using UnityEngine;

public class WellInteractable : MonoBehaviour, IInteractable {
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerInteract playerInteract)) {
            playerInteract.interactable = this;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerInteract playerInteract)) {
            if (playerInteract.interactable is WellInteractable wellInteractable && wellInteractable == this) {
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

        string[] dialogues = new string[] {
            "The well is dry.",
            "Please go back to the healing spring in the store."
        };
        dialogueUI.ShowDialogue(dialogues);
    }
}
