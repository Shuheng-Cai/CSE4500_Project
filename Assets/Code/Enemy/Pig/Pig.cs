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
    public RuntimeAnimatorController angryPig;
    private bool isAngry = false;

    // TODO:Animation change
    protected override void Move()
    {
        Vector2 direction = (target - transform.position).normalized;
        if (currentHealth <= enemyData.maxHealth / 2 && !isAngry)
        {
            speed = speed * 1.2f;
            isAngry = true;
            transform.GetComponent<Animator>().runtimeAnimatorController = angryPig;
        }
        _rb.AddForce(direction * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

}
