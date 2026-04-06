using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateFloatingText(string damageText, Vector3 position)
    {
        Debug.Log($"Creating floating text: '{damageText}' at position {position}");
        if (TextPool.instance == null)
        {
            Debug.LogError("TextManager requires an active TextPool in the scene.", this);
            return;
        }

        IFloatingText text = TextPool.instance.Get();
        if (text == null)
        {
            return;
        }

        text.SetPosition(position);
        text.SetText(damageText);
        text.FloatingText();
    }

    public void ReturnFloatingText(IFloatingText floatingText)
    {
        if (floatingText == null || TextPool.instance == null)
        {
            return;
        }

        TextPool.instance.Return(floatingText);
    }
}
