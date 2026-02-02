// ==================================================
// Module: Pig
// Purpose: first enemy; speed up after life half
// Author: Shuheng
// Date: 2025/10/11
// Dependencies: Enemy
// ==================================================


using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

public class Pig : Enemy
{
    private bool isAngry = false;

    // TODO:Animation change
    protected override void Move()
    {
        Vector3 direction = (target - transform.position).normalized;
        if (currentHealth < enemyData.maxHealth && !isAngry)
        {
            speed = speed * 1.2f;
            isAngry = true;
            animator.SetBool("isAngry", true);
        }
        transform.position += direction * speed * Time.deltaTime;
    }

}
