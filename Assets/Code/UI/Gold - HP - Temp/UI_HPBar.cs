using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour
{
    public Image bloodFill;
    public RectTransform barTransform;

    [Header("Pixel HP Settings")]
    public int totalSteps = 22;

    [Header("Shake Settings")]
    public float lowHealthThreshold = 0.25f;
    public float shakeAmount = 6f;
    public float shakeSpeed = 25f;

    private Vector2 originalAnchoredPosition;

    void Start()
    {
        if (barTransform != null)
        {
            originalAnchoredPosition = barTransform.anchoredPosition;
        }
    }

    void Update()
    {
        if (PlayerManager.instance == null || bloodFill == null) return;

        float curHP = PlayerManager.instance.currentHealth;
        float maxHP = PlayerManager.instance.MaxHealth;

        if (maxHP <= 0f) return;

        float ratio = Mathf.Clamp01(curHP / maxHP);

        // ===== 关键：22格离散血条 =====
        int step = Mathf.CeilToInt(ratio * totalSteps);
        step = Mathf.Clamp(step, 0, totalSteps);

        float steppedRatio = (float)step / totalSteps;

        bloodFill.fillAmount = steppedRatio;
        // =================================

        // 抖动逻辑（用离散后的血量更稳定）
        if (barTransform != null)
        {
            if (steppedRatio <= lowHealthThreshold && steppedRatio > 0f)
            {
                float offsetX = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
                barTransform.anchoredPosition = originalAnchoredPosition + new Vector2(offsetX, 0f);
            }
            else
            {
                barTransform.anchoredPosition = originalAnchoredPosition;
            }
        }
    }
}