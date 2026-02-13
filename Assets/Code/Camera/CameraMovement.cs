using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    void FixedUpdate()
    {
       transform.position += new Vector3(0.1f, 0.1f, 0f);
    }
}
