using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    // Outlets
    Rigidbody2D _rb;
    public Tilemap _tilemap;
    public Vector3 testCell;

    // Configuration
    public float speed;

    // Method
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        testCell = WorldToCellPosition();
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

    // Get the Tilemap position
    public Vector3 WorldToCellPosition()
    {
        Vector3 cellPos = _tilemap.WorldToCell(transform.position);

        return cellPos;
    }
}
