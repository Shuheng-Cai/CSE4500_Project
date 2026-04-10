using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeUpgradeArea : MonoBehaviour
{
    public GameObject upgradeUI;
    // Method
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            upgradeUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            upgradeUI.SetActive(false);
        }
    }
}
