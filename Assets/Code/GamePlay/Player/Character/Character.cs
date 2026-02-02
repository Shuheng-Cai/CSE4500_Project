using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Outlet
    Animator animator;
    Rigidbody2D _rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        animator.SetFloat("Speed", _rb.velocity.magnitude);
    }
}
