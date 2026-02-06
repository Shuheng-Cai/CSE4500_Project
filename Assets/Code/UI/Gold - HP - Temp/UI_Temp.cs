using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Temp : MonoBehaviour
{
    private TMP_Text textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = $"Gold: {GoldManager.instance.currentGold}";
    }
}
