using UnityEngine;

[CreateAssetMenu(menuName = "Game/Dialogue")]
public class DialogueData : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogues;
    [SerializeField] private Response[] responses;

    public bool HasResponses => Responses != null && Responses.Length > 0;
    public string[] Dialogues => dialogues;
    public Response[] Responses => responses; 
}
