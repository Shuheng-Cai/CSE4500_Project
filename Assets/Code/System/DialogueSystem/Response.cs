using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseTextTitle;
    [SerializeField] private DialogueData dialogueData;

    public string ResponseTextTitle => responseTextTitle;
    public DialogueData DialogueData => dialogueData;
}
