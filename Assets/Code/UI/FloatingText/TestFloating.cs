using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFloating : MonoBehaviour
{
    void Start()
    {
        if (TextManager.instance == null)
        {
            Debug.LogError("TestFloating requires a TextManager in the scene.", this);
            return;
        }

        TextManager.instance.CreateFloatingText("100", transform.position);
    }
}
