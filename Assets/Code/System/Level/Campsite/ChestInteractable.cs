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
        Debug.Log("Chest opened!");
    }
}
