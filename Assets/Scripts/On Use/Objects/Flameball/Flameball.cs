using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Flameball : MonoBehaviour
{
    public float Damage;
    private float travelSpeed = 3f;
    bool directionX;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Transform Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            collision.GetComponent<PlayerMovement>().TakeDamage(Damage);
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        Transform boss = FindObjectOfType<Boss01>().gameObject.GetComponent<Transform>();
        spriteRenderer = boss.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<PlayerMovement>().gameObject.transform;

    }

    private void FixedUpdate()
    {
        /*        if (directionX)
                    transform.position = new Vector2(transform.position.x - Time.fixedDeltaTime * travelSpeed, transform.position.y);
                else
                    transform.position = new Vector2(transform.position.x + Time.fixedDeltaTime * travelSpeed, transform.position.y);*/
        GetComponent<SpriteRenderer>().flipY = !spriteRenderer.flipX;
        Vector2 newPos = Vector2.MoveTowards(transform.position, Player.position, Time.fixedDeltaTime * travelSpeed);
        rb.MovePosition(newPos);
    }
}
