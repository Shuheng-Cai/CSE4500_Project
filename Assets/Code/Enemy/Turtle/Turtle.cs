using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

public class Turtle : Enemy
{
    private bool isAngry = false;

    protected override void Move()
    {
        // Todo for Eva: Debug.Log("hello");
        // Stop if hit maybe retract body into shell
        Vector3 direction = (target - transform.position).normalized;
        if (currentHealth < enemyData.maxHealth && !isAngry)
        {
            speed = 0;
            isAngry = true;
            animator.SetBool("isAngry", true);
            StartCoroutine(ResumeMovement());
        }
        transform.position += direction * speed * Time.deltaTime;
    }
}
