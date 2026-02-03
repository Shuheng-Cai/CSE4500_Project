using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public CharacterData currentCharacter;
    public GameObject playerPrefab;
    public GameObject player {get; private set;}

    // Configuration
    public int MaxHealth {get ; private set;}

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
        player.GetComponent<Animator>().runtimeAnimatorController = currentCharacter.CharacterAnimController;
        DontDestroyOnLoad(player);
    }

    public void ResetPlayerPosition()
    {
        player.transform.position = Vector2.zero;
    }
}
