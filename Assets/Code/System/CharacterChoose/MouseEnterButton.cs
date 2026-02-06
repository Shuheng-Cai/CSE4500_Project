using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEnterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isEnter;
    public CharacterPanel panel;
    public CharacterData characterData;

    public void OnPointerEnter(PointerEventData eventData)
    {
        RectTransform rectTransform = (RectTransform)transform;
        isEnter = true;
        panel.Show(characterData,  rectTransform.anchoredPosition);
        transform.Find("Character")?.gameObject.GetComponent<Animator>().SetFloat("speed", 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
        panel.Hide();
        transform.Find("Character")?.gameObject.GetComponent<Animator>().SetFloat("speed", 0f);
    }
}
