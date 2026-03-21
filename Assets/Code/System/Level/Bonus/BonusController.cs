using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour {

    private float testWaitTime = 5f;
    public BonusToStore exitToStore;

    public Sprite closedDoor;
    public Sprite openDoor;
    public SpriteRenderer door;

    // Start is called before the first frame update
    void Start()
    {
        door.sprite = closedDoor;
        exitToStore.SetCanExit(false);

        StartCoroutine(ExitBonus());
    }

    private IEnumerator ExitBonus() {
        yield return new WaitForSeconds(testWaitTime);
        
        door.sprite = openDoor;
        exitToStore.SetCanExit(true);
    }
}
