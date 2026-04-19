using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseTextTitle;
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private string name;

    public string ResponseTextTitle => responseTextTitle;
    public string Name => name;
    public DialogueData DialogueData => dialogueData;
}
