using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrintSeed : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var text = GetComponent<TMP_Text>();
        text.text = GameContext_Seed.MazeSeed.ToString();
    }
}
