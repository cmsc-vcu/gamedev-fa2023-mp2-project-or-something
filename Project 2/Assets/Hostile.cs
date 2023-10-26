using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostile : MonoBehaviour
{
    GameObject plyaer;
    GameObject owner;
    Transform _transform;
    private Transform target;   // Player
    private Rigidbody2D enemy;

    private float MOVESPEED = 9;
    private float ACCEL = 0.25f;
    private float DECEL = 0.10f;
    private Vector2 moveDirection = Vector2.zero;

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
    void Update()
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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthSystem>().hit(false);
        }
    }
}
