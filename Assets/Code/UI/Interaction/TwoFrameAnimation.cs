using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoFrameAnimation : MonoBehaviour
{
    public Sprite spriteA;
    public Sprite spriteB;
    public float interval = 0.2f;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        InvokeRepeating(nameof(SwitchSprite), 0, interval);
    }

    void SwitchSprite()
    {
        sr.sprite = sr.sprite == spriteA ? spriteB : spriteA;
    }
}
