using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusToStore : MonoBehaviour {

    private bool canExit = false;

    public void SetCanExit(bool canExit) {
        this.canExit = canExit;
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (!canExit) return;
        
        if (other.CompareTag("Player")) {
            GameManager.instance.EnterStore();
        }
    }
}
