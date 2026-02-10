using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeArea : MonoBehaviour
{
    bool canUpgrade;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(PlayerUpgrade());
            canUpgrade = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        canUpgrade = false;
    }

    IEnumerator PlayerUpgrade()
    {
        GameEvent.OnPlayerUpgrade.Invoke();
        yield return new WaitForSeconds(0.5f);

        if(canUpgrade)
            StartCoroutine(PlayerUpgrade());
    }
}
