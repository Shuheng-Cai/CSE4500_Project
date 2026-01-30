using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Ghost : Enemy
{
    // State Tracking
    public float appearTime = 3f;
    public float disappearTime = 0.3f;
    private float angleOffset = 30f;
    private float timeOffset = 0;

    // Method

    void Start()
    {
        StartCoroutine(DisappearRoutine());
    }

    void FixedUpdate()
    {
        timeOffset++;
        if(timeOffset >= 25)
        {
            timeOffset = 0;
            angleOffset = angleOffset * -1;
        }
    }

    protected override void Move()
    {
        Vector3 direction = (target - transform.position).normalized;
        Vector3 offsetDir = Quaternion.AngleAxis(angleOffset, Vector3.forward) * direction;

        transform.position += offsetDir * speed * Time.deltaTime;
    }

    // Appear and Disappear
    public void Disappear()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public void Appear()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;
    }

    private IEnumerator DisappearRoutine()
    {
        animator.SetTrigger("disappear");
        yield return new WaitForSeconds(disappearTime);
        yield return new WaitForSeconds(appearTime);
        StartCoroutine(DisappearRoutine());
    }
}
