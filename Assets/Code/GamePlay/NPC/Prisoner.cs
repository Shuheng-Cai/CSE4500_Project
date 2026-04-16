using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisioner : MonoBehaviour
{
    DialogueActivator dialogueActivator;
    public DialogueData afterBossFight;


    void Start()
    {
        dialogueActivator = GetComponent<DialogueActivator>();
    }

    void Update()
    {
        if (dialogueActivator != null && PlayerManager.instance.isBossFight)
        {
            dialogueActivator.dialogueData = afterBossFight;
        }
    }
}
