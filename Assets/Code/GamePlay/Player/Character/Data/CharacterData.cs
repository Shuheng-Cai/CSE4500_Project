using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Character") ]
public class CharacterData : ScriptableObject
{
    public RuntimeAnimatorController CharacterAnimController;
    public float BaseSpeed;
    public float BaseMaxHealthPoint;
    public float BaseStrength;
    public float StartCoin;
    public BulletData BaseBullet;
}
