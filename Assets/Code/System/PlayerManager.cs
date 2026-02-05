using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    // Outlet
    public CharacterData currentCharacter;
    public GameObject playerPrefab;

    // Make it List
    public List<BulletData> currentBullets;
    public List<BulletData> currentLasers;
    public GameObject player {get; private set;}

    // Configuration
    public int MaxHealth {get ; private set;}
    public bool enterBattle {get; private set;}

    // Method
    void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        MaxHealth = currentCharacter.BaseMaxHealthPoint;
    }

    public void ChangeCharacter(CharacterData character)
    {
        currentCharacter = character;   
    }

    public void PlayerGenerate()
    {
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        if (currentCharacter.BaseBullet.isLaser)
        {
            currentLasers.Add(currentCharacter.BaseBullet);
        }

        else 
        {
            currentBullets.Add(currentCharacter.BaseBullet);
        }

        player.GetComponent<Animator>().runtimeAnimatorController = currentCharacter.CharacterAnimController;
        DontDestroyOnLoad(player);
    }

    // TODO: enter battle in other place
    public void ResetPlayerPosition()
    {
        player.transform.position = Vector2.zero;
        enterBattle = true;
    }

    public void EnterBattle()
    {
        enterBattle = false;
    }
}
