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
        isHit();
    }

    public void isHit()
    {
        if (PlayerManager.instance.invulnerable)
        {
            animator.SetTrigger("isHit");
        }
    }

    public void Moving()
    {
        animator.SetFloat("speed", _rb.velocity.magnitude);
    }
}
