using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatFloatingText : MonoBehaviour, IFloatingText
{
    public float moveSpeed = 1f;
    public float lifeTime = 1f;

    private TextMeshProUGUI text;
    private Color startColor;
    private Vector3 startPosition;
    private Coroutine activeAnimation;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>(true);
        if (text == null)
        {
            Debug.LogError("CombatFloatingText requires a TextMeshProUGUI in this object hierarchy.", this);
            enabled = false;
            return;
        }

        startColor = text.color;
        startPosition = transform.localPosition;
    }

    public void FloatingText()
    {
        if (!enabled)
        {
            return;
        }

        if (activeAnimation != null)
        {
            StopCoroutine(activeAnimation);
        }

        activeAnimation = StartCoroutine(StartFloat());
    }

    IEnumerator StartFloat()
    {
        float elapsed = 0f;
        Color color = startColor;
        transform.localPosition = startPosition;

        while (elapsed < lifeTime)
        {
            float progress = elapsed / lifeTime;
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            color.a = Mathf.Lerp(startColor.a, 0f, progress);
            text.color = color;

            elapsed += Time.deltaTime;
            yield return null;
        }

        activeAnimation = null;
        TextManager.instance.ReturnFloatingText(this);
    }

    public void SetText(string damageText)
    {
        transform.gameObject.SetActive(true);
        text.text = damageText;
        text.color = startColor;
        transform.localPosition = startPosition;
    }

    public void Return()
    {
        if (activeAnimation != null)
        {
            StopCoroutine(activeAnimation);
            activeAnimation = null;
        }

        text.color = startColor;
        transform.localPosition = startPosition;
        transform.gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
