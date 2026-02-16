using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel; 
    [SerializeField] private DialogueData testDialogue;

    public bool isOpen {get; private set;}

    private TypewriteEffect typewriteEffect;
    private ResponseHandler responseHandler;

    void Start()
    {
        typewriteEffect = GetComponent<TypewriteEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        //ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueData dialogueData)
    {
        isOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueData));
    }

    private IEnumerator StepThroughDialogue(DialogueData dialogueData)
    {
        for(int i = 0; i < dialogueData.Dialogues.Length; i++)
        {
            string dialogue = dialogueData.Dialogues[i];

            yield return RunTypingEffect(dialogue);

            yield return null;
            
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));

            yield return null;  

            if(i == dialogueData.Dialogues.Length - 1 && dialogueData.HasResponses) break;
        }

        if (dialogueData.HasResponses)
        {
            responseHandler.ShowResponses(dialogueData.Responses);
        }

        else
        {   
            CloseDialogueBox();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriteEffect.Run(dialogue, textLabel);

        while (typewriteEffect.isRunning)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                typewriteEffect.Stop(dialogue, textLabel);
                break;
            }
            yield return null;
        }
    }

    private void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
