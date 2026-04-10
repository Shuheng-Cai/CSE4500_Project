using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour
{
    public Image HPBarFill;

    private Color High = new Color(0f, 0.933f, 0f);
    private Color Mid = new Color(1f, 0.853f, 0f);
    private Color Low = new Color(1f, 0.250f, 0.250f);

    void OnEnable()
    {
        gameObject.SetActive(true);
    }
    void Awake()
    {
        gameObject.SetActive(true);
        Debug.Log("UI_HPBar Awake");
    }

    void Start()
    {
        gameObject.SetActive(true);
        Debug.Log("UI_HPBar Start");
    }

    // Update is called once per frame
    void Update() {
        //if (PlayerManager.instance == null) return;

        float curHP = PlayerManager.instance.currentHealth;
        float maxHP = PlayerManager.instance.MaxHealth;
        
        float ratio = curHP / maxHP;
        
        HPBarFill.fillAmount = ratio;

        if (ratio >= 0.5f) {
            HPBarFill.color = High;
        }else if (ratio >= 0.25f) {
            HPBarFill.color = Mid;
        }
        else {
            HPBarFill.color = Low;
        }
    }
}
