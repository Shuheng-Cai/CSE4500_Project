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
    public float MaxHealth {get ; private set;}
    public bool enterBattle;

    // StateTracking
    public bool invulnerable = false;
    public bool playerAlive;

    // Method
    void Awake()
    {
        instance = this;
    }

    public void ChangeCharacter(CharacterData character)
    {
        currentCharacter = character;
        GoldManager.instance.AddCoin(character.StartCoin);
        MaxHealth = currentCharacter.BaseMaxHealthPoint; 
        Debug.Log(character.StartCoin);
    }

    public void PlayerGenerate()
    {
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerAlive = true;

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
        new WaitForSeconds(1f);
        enterBattle = true;
    }

    public void EnterStore()
    {
        player.transform.position = Vector2.zero;
    }

    public void EnterBattle()
    {
        enterBattle = false;
    }

    public void Die()
    {
        playerAlive = false;
        GameManager.instance.EnterGameOver();
        Destroy(player);
    }

    public void SetPlayerInvulnerable()
    {
        invulnerable = true;
        new WaitForSeconds(0.5f);
        invulnerable = false;
    }
}
