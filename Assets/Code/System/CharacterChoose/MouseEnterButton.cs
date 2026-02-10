using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEnterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isEnter;
    public CharacterData characterData;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isEnter = true;
        transform.Find("Character")?.gameObject.GetComponent<Animator>().SetFloat("speed", 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
        transform.Find("Character")?.gameObject.GetComponent<Animator>().SetFloat("speed", 0f);
    }
}
