using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Enemy
{
    public float timeBeforeFirstChargeAttack = 10f;
    public float chargeAttackWarningTime = 1f;
    public float chargeAttackSpeedMultiplier = 2f;
    public float chargeAttackDamageMultiplier = 2f;
    
    protected override void Move()
    {

        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
    
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(ChargeAttack());
        
    }

    IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(timeBeforeFirstChargeAttack);

        speed = 0;
        animator.SetBool("isMove", false);
        
        Color originalColor = sprite.color;
        sprite.color = Color.red;
        
        yield return new WaitForSeconds(chargeAttackWarningTime);
        
        sprite.color = originalColor;
        speed = enemyData.speed * chargeAttackSpeedMultiplier;
        damage = enemyData.damage * chargeAttackDamageMultiplier;
        
        animator.SetBool("isMove", true);
    }
    
}
