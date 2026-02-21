using UnityEngine;

public class StoreToCampsite : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.instance.EnterCampsite();
        }
    }
}
