using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level_UI : MonoBehaviour
{
    // Start is called before the first frame update
    private TMP_Text textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = $"Level: {GameManager.instance.currentLevel}";
    }
}
