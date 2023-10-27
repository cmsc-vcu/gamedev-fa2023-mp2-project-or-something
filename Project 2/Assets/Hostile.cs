using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Hostile : MonoBehaviour
{
    GameObject plyaer;
    GameObject owner;
    Transform _transform;
    private Transform target;   // Player
    private Rigidbody2D enemy;
    public Sprite[] run;
    public int index = 0;

    private float MOVESPEED = 6;
    private float ACCEL = 1.5f;
    private float DECEL = 1.5f;
    private Vector2 moveDirection = Vector2.zero;

    private float animTick = 0f;
    private float animTickLimit = 0.06f;

    void Awake()
    {
        _transform = transform;
        owner = _transform.gameObject;
        plyaer = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<Rigidbody2D>();
        target = plyaer.transform;
        GetComponent<HealthSystem>().setAttributes(20, owner);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (target.position.x - enemy.position.x > 0)
        {
            moveDirection.x = 1;
        }
        else
        {
            moveDirection.x = -1;
        }

        if (target.position.y - enemy.position.y > 0)
        {
            moveDirection.y = 1;
        }
        else
        { 
            moveDirection.y = -1;
        }

        // Roam
        float targetSpeedx = moveDirection.x * MOVESPEED;
        float speedDiffx = targetSpeedx - enemy.velocity.x;
        float acceleratex = (Mathf.Abs(targetSpeedx) > 0.01f) ? ACCEL : DECEL;
        float Roamx = Mathf.Pow(Mathf.Abs(speedDiffx) * acceleratex, 1.6f) * Mathf.Sign(speedDiffx/2);

        float targetSpeedy = moveDirection.y * MOVESPEED;
        float speedDiffy = targetSpeedy - enemy.velocity.y;
        float acceleratey = (Mathf.Abs(targetSpeedy) > 0.01f) ? ACCEL : DECEL;
        float Roamy = Mathf.Pow(Mathf.Abs(speedDiffy) * acceleratey, 1.6f) * Mathf.Sign(speedDiffy/2);

        enemy.AddForce(Roamx * Vector2.right);
        enemy.AddForce(Roamy * Vector2.up);

        

        // Changes character direction and 'animates'
        if (moveDirection.x > 0)
        {
            owner.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (moveDirection.x < 0)
        {
            owner.GetComponent<SpriteRenderer>().flipX = true;
        }
        owner.GetComponent<Transform>().rotation = Quaternion.identity;

        animTick += Time.deltaTime;
        if (animTick > animTickLimit)
        {
            animTick = 0f;
            owner.GetComponent<SpriteRenderer>().sprite = run[index++];
        }

        if (index > 3)
        {
            index = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthSystem>().hit(false);
        }
    }
}
