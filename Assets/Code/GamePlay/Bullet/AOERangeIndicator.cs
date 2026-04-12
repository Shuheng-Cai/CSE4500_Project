using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOERangeIndicator : MonoBehaviour
{
    public float radius = 3f;
    public float duration = 0.5f;
    public Color color = new Color(1f, 0f, 0f, 0.3f);
    public bool autoFade = true;
    
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null)
        {
            float spriteRadius = spriteRenderer.sprite.bounds.extents.x;
            float scale = radius / spriteRadius;
            transform.localScale = new Vector3(scale, scale, 1f);
            
            spriteRenderer.color = color;

            if (autoFade) {
                StartCoroutine(FadeOut());
            }
            
        }
    }
    
    IEnumerator FadeOut()
    {
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(color.a, 0f, elapsed / duration);
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
