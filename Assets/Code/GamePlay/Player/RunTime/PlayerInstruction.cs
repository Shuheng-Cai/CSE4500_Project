using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstruction : MonoBehaviour
{
    private GameObject instruction;

    void Start()
    {
        instruction = transform.Find("Interaction_E").gameObject;
        instruction.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Interactable")
            instruction.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        instruction.SetActive(false);
    }
}
