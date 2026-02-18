using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

public class Bunny : Enemy {
    private bool isAngry = false;

    // Hopping
    private float hopDuration = 0.3f;
    private float landDuration = 0.15f;
    private float hopHeight = 0.5f;

    // Hopping state management
    private float hopTimer = 0f;
    private bool isHopping = true;
    private Vector3 groundPos;
    private bool initialized = false;

    protected override void Move() {
        if (!initialized) {
            groundPos = transform.position;
            initialized = true;
        }

        if (currentHealth < enemyData.maxHealth && !isAngry) {
            speed = speed * 1.2f;
            isAngry = true;
            animator.SetBool("isAngry", true);
        }

        hopTimer += Time.deltaTime;

        if (isHopping) {
            float hopProgress = hopTimer / hopDuration;

            if (hopProgress >= 1f) {
                // Once the hop is completed, enter the landing phase
                transform.position = groundPos;
                isHopping = false;
                hopTimer = 0f;
                return;
            }

            // Move ground pos along a straight path toward target so we
            // move in an overall linear path while maintianing repeated arc path
            Vector3 direction = (target - groundPos).normalized;
            groundPos += direction * speed * Time.deltaTime;

            // Bunny at the ground position + arc of vertical hop
            float yOffset = Mathf.Sin(hopProgress * Mathf.PI) * hopHeight;
            transform.position = groundPos + new Vector3(0f, yOffset, 0f);
        } else {
            // If we aren't mid-hop, we are in the land position
            if (hopTimer >= landDuration) {
                isHopping = true;
                hopTimer = 0f;
            }
        }
    }
}
