using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    // Outlets
    Rigidbody2D _rb;
    public Tilemap _tilemap;

    // Configuration
    private float speed;

    // Method
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        speed = PlayerManager.instance.currentCharacter.BaseSpeed;
    }

    // Control player to move
    void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _rb.AddForce(Vector2.left * speed * Time.deltaTime, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _rb.AddForce(Vector2.right * speed * Time.deltaTime, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.W))
        {
            _rb.AddForce(Vector2.up * speed * Time.deltaTime, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.S))
        {
            _rb.AddForce(Vector2.down * speed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
