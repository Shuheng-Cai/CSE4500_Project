using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriteEffect : MonoBehaviour
{
    [SerializeField] private float textTypeSpeed = 10f;

    public bool isRunning {get; private set;}

    private readonly Dictionary<HashSet<char>, float> punctuations = new Dictionary<HashSet<char>, float>()
    {
        {new HashSet<char>(){'.', '?'}, 0.6f},
        {new HashSet<char>(){',', ';', ':'}, 0.3f},
        {new HashSet<char>(){'!'}, 0.1f}
    };


    private Coroutine typingCoroutine;
    public void Run(string textToType, TMP_Text textLabel)
    {
        typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
    }

    public void Stop(string textToType, TMP_Text textLabel)
    {
        StopCoroutine(typingCoroutine);
        isRunning = false;
        textLabel.text = textToType;
    }
    
    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        isRunning = true;
        textLabel.text = string.Empty;

        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * textTypeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            for(int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i <= textToType.Length - 1;

                textLabel.text = textToType.Substring(0, i + 1);

                if(IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }

            yield return null;
        }

        isRunning = false;
    }

    private bool IsPunctuation(char c, out float waitTime)
    {
        foreach (var punction in punctuations)
        {
            if (punction.Key.Contains(c))
            {
                waitTime = punction.Value;
                return true;
            }
        }

        waitTime = default;
        return false;
    }
}
