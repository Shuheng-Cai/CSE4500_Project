using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seller : MonoBehaviour
{
    public DialogueActivator dialogueActivator;
    public GameObject sellerUI;

    void Start()
    {
        dialogueActivator = GetComponent<DialogueActivator>();
        if(dialogueActivator != null)
        {
            dialogueActivator.onDialogueStarted += OnDialogueStarted;
        }
    }

    private void OnDestroy()
    {
        if(dialogueActivator != null)
        {
            dialogueActivator.onDialogueStarted -= OnDialogueStarted;
        }
    }

    private void OnDialogueStarted()
    {
        // Do something when the dialogue with the seller starts
        Debug.Log("Dialogue with the seller has started.");
        // Open the seller UI / shop here
        if(sellerUI != null)
        {
            sellerUI.SetActive(true);
        }
    }

    public void BuyItem()
    {
        if (GoldManager.instance.CostCoin(10f))
        {
            PlayerManager.instance.ItemAdd();
            sellerUI.SetActive(false);
        }
    }

    public void CloseSellerUI()
    {
        if(sellerUI != null)
        {
            sellerUI.SetActive(false);
        }
    }
}
