using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealArea : MonoBehaviour
{
    // Method
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerManager.instance.IsUpgrading();
            PlayerManager.instance.inUpgradeArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        PlayerManager.instance.inUpgradeArea = false;
    }
}
