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
    public List<BulletData> currentNormalBullets;
    public List<BulletData> currentLasers;
    public GameObject player {get; private set;}

    // Configuration

    // Player Attributes
    [Header("Player Attribute")]
    public float MaxHealth;
    public float currentHealth;
    public float Speed;
    public float Strength;

    // StateTracking
    public bool invulnerable = false;
    public bool playerAlive;
    public bool canShoot;
    public bool inUpgradeArea = false;
    public bool inHealingArea = false;
    
    // Method
    void Awake()
    {
        instance = this;
    }

    // Change the character of Player
    public void ChangeCharacter(CharacterData character)
    {
        currentCharacter = character;
        GoldManager.instance.AddCoin(character.StartCoin);

        MaxHealth = currentCharacter.BaseMaxHealthPoint; 
        Strength = currentCharacter.BaseStrength;

        Debug.Log(character.StartCoin);
    }

    // Generate Player and add different bullet type to list
    public void PlayerGenerate()
    {
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        currentHealth = MaxHealth;
        playerAlive = true;

        if (currentCharacter.BaseBullet.bulletType == BulletType.Laser)
        {
            currentLasers.Add(currentCharacter.BaseBullet);
        }

        else 
        {
            currentNormalBullets.Add(currentCharacter.BaseBullet);
        }

        player.GetComponent<Animator>().runtimeAnimatorController = currentCharacter.CharacterAnimController;
        DontDestroyOnLoad(player);
    }

    public void ResetPlayerInBattle()
    {
        player.transform.position = Vector2.zero;
        canShoot = true;
        GameEvent.ShootEachBattleLevel.Invoke();
    }

    // Enter other level
    public void EnterStore()
    {
        player.transform.position = Vector2.zero;
        canShoot = false;
    }

    public void EnterBattle()
    {
        canShoot = true;
    }

    // Player Hit -> Player Invulnerable
    public IEnumerator SetPlayerInvulnerable()
    {
        invulnerable = true;
        player.GetComponent<PlayerAnim>().isHit();
        yield return new WaitForSeconds(1f);
        invulnerable = false;
    }

    // When Player Die
    public void Die()
    {
        playerAlive = false;
        GameManager.instance.EnterGameOver();
        Destroy(player);
    }

    // player heal / upgrade
    public void IsHealing()
    {
        StartCoroutine(PlayerHeal());
    }

    public IEnumerator PlayerHeal()
    {
        if(GoldManager.instance.CostCoin(0.1f) && currentHealth < MaxHealth)
        {
            currentHealth = currentHealth + 1 < MaxHealth ? currentHealth + 1 : MaxHealth;
            yield return new WaitForSeconds(0.1f);
            if (inHealingArea)
                StartCoroutine(PlayerHeal());
        }
    }

    public void IsUpgrading()
    {
        StartCoroutine(PlayerUpgrade());
    }

    IEnumerator PlayerUpgrade()
    {
        GameEvent.OnPlayerUpgrade.Invoke();
        yield return new WaitForSeconds(0.5f);
        if (inUpgradeArea)
            StartCoroutine(PlayerUpgrade());
    }

    // TakeDamage
    public void TakeDamage(float damage)
    {
        if (!invulnerable)
        {
            currentHealth -= damage;
            if(currentHealth < 0)
            {
                Die();
            }
            StartCoroutine(SetPlayerInvulnerable());
        }
    }
}
