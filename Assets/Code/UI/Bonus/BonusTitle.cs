using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTitle : MonoBehaviour {

    public float displayTime = 2f;
        
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(ShowTitle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShowTitle() {
        yield return new WaitForSeconds(displayTime);
        Destroy(gameObject);
    }
}
