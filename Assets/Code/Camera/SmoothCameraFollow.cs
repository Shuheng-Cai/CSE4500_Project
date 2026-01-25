using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    // Outlet
    public Transform _Follow;

    // Configuration
    public float smoothSpeed = 8f;

    // State Tracking
    private Vector3 offset;

    // Method

    void Awake()
    {
        if (_Follow == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p) _Follow = p.transform;
        }
    }
    void Start()
    {
        offset = transform.position - _Follow.position;
    }

    void LateUpdate()
    {
        var target = new Vector3(_Follow.position.x, _Follow.position.y, transform.position.z);
        transform.position = Vector3.Lerp(
            transform.position,
            target,
            smoothSpeed * Time.deltaTime
        );
    }
}
