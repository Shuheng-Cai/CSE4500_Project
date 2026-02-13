using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Outlets
    public static PlayerState instance;

    void Start()
    {
        instance = this;
    }
}
