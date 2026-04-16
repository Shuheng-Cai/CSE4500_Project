using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPoint : MonoBehaviour
{
    public TMPro.TextMeshProUGUI pointText;
    void Start()
    {
        pointText = GetComponent<TMPro.TextMeshProUGUI>();
    }
    void Update()
    {
        pointText.text = PointManager.instance.Points.ToString();
    }
}
