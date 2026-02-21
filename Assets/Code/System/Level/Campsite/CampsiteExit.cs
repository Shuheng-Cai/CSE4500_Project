using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampsiteExit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.instance.EnterStore();
        }
    }
}
