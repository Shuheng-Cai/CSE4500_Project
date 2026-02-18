using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeUpgradeArea : MonoBehaviour
{
    // Method
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerManager.instance.inUpgradeArea = true;
            PlayerManager.instance.IsUpgrading();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        PlayerManager.instance.inUpgradeArea = false;
    }
}
