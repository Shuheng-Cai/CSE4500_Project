using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Ghost : Enemy
{
    // State Tracking
    public float appearTime = 3f;
    public float disappearTime = 0.3f;
    private bool canDisappear = true;
    private float angleOffset = 30f;
    private float timeOffset = 0;

    // Method
    protected override void Move()
    {
        Vector3 direction = (target - transform.position).normalized;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 offsetDir = Quaternion.AngleAxis(angleOffset, Vector3.forward) * direction;

        transform.position += offsetDir * speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        timeOffset++;
        if(timeOffset >= 25)
        {
            timeOffset = 0;
            angleOffset = angleOffset * -1;
        }

        if (canDisappear)
        {
            StartCoroutine(DisappearRoutine());
        }
    }

    public void Disappear()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Appear()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    // Ghost disappear and appear.
    private IEnumerator DisappearRoutine()
    {
        Disappear();
        animator.SetTrigger("disappear");
        canDisappear = false;
        yield return new WaitForSeconds(disappearTime);
        Appear();
        animator.SetTrigger("appear");
        yield return new WaitForSeconds(appearTime);
        canDisappear = true;
    }
}
