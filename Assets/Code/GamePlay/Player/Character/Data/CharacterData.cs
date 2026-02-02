using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Character") ]
public class CharacterData : ScriptableObject
{
    public RuntimeAnimatorController CharacterAnimController;
    public int BaseSpeed;
    public int BaseMaxHealthPoint;
    public int BaseDamage;
    public int StartCoin;
}
