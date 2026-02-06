using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

public class Bunny : Enemy {
    private bool isAngry = false;

    protected override void Move() {
        Vector3 direction = (target - transform.position).normalized;
        if (currentHealth < enemyData.maxHealth && !isAngry) {
            speed = speed * 1.2f;
            isAngry = true;
            animator.SetBool("isAngry", true);
        }
        transform.position += direction * speed * Time.deltaTime;
    }
}
