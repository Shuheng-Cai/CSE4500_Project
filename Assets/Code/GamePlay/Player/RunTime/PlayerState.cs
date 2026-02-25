using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This is going to be deleted. Enemy depend on it.
*/
public class PlayerState : MonoBehaviour
{
    // Outlets
    public static PlayerState instance;

    void Start()
    {
        instance = this;
    }
}
