using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMushroom : Enemy
{
    protected override void Move() {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
