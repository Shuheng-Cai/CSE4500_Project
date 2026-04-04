using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPool : MonoBehaviour
{
    public static TextPool instance;
    private readonly Queue<IFloatingText> floatingTexts = new Queue<IFloatingText>();
    [SerializeField] private GameObject text;
    [SerializeField] private int maxTextInstances = 30;
    private int createdCount;

    public GameObject worldCanvas;

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

    public IFloatingText Get()
    {
        if(floatingTexts.Count == 0)
        {
            if (createdCount >= maxTextInstances)
            {
                return null;
            }

            IFloatingText createdText = CreateInstance();
            if (createdText == null)
            {
                return null;
            }

            floatingTexts.Enqueue(createdText);
        }

        IFloatingText target = floatingTexts.Dequeue();

        return target;
    }

    public void Return(IFloatingText floatingText)
    {
        if (floatingText == null)
        {
            return;
        }

        floatingText.Return();
        floatingTexts.Enqueue(floatingText);
    }

    private IFloatingText CreateInstance()
    {
        if (text == null)
        {
            Debug.LogError("TextPool is missing its floating text prefab reference.", this);
            return null;
        }

        if (worldCanvas == null)
        {
            Debug.LogError("TextPool is missing its worldCanvas reference.", this);
            return null;
        }

        GameObject gameObject = Instantiate(text, worldCanvas.transform);
        IFloatingText floatingText = gameObject.GetComponent<IFloatingText>();

        if (floatingText == null)
        {
            Debug.LogError($"Floating text prefab '{text.name}' must have a component that implements IFloatingText on its root object.", text);
            Destroy(gameObject);
            return null;
        }

        createdCount++;
        gameObject.SetActive(false);
        return floatingText;
    }
}
