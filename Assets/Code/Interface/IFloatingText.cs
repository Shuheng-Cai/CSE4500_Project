using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloatingText
{
    void FloatingText();
    void SetText(string text);
    void Return();
    void SetPosition(Vector3 position);
}
