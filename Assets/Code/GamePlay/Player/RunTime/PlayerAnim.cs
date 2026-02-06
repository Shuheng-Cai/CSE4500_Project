using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // Outlet
    public Animator animator;
    private Rigidbody2D _rb;

    // Method

    void Start()
    {
        animator = GetComponent<Animator>();
        _rb = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Moving();
    }

    public void isHit()
    {
        animator.SetTrigger("isHit");
    }

    public void Moving()
    {
        animator.SetFloat("speed", _rb.velocity.magnitude);
    }
}
