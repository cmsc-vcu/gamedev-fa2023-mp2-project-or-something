using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerInput : MonoBehaviour
{
    /*****
     * Variables (Changed in runtime)
     *****/

    // Player States
    Vector2 moveDirection = Vector2.zero;
    Vector2 mousePosition = Vector2.zero;

    /*****
     * Constants (Changed in editor)
     *****/

    // Roam Speeds
    // private float SLOWSPEED = 5; // Slow
    private float MOVESPEED = 8; // Nomral (Player Default)
    // private float FASTSPEED = 12; // Fast
    // private float BLITZSPEED = 20; // Blitz (Projectile)

    // Accelration and Decceleration
    private float ACCEL = 2f;
    private float DECEL = 3f;

    /*****
     * Pure Constant (Not changed ever)
     *****/

    // Player
    GameObject owner;
    Transform _transform;
    public Rigidbody2D player2D;
    public PlayerInput inputActions;
    public GameObject debug;

    // Starts when script is compiled or something
    void Awake()
    {
        _transform = transform;
        owner = _transform.gameObject;
        inputActions = new PlayerInput();
        player2D = GetComponent<Rigidbody2D>();
        GetComponent<HealthSystem>().setAttributes(100, owner);
    }

    // Update
    void FixedUpdate()
    {

        // Roam
        float targetSpeedx = moveDirection.x * MOVESPEED;
        float speedDiffx = targetSpeedx - player2D.velocity.x;
        float acceleratex = (Mathf.Abs(targetSpeedx) > 0.01f) ? ACCEL : DECEL;
        float Roamx = Mathf.Pow(Mathf.Abs(speedDiffx) * acceleratex, 2) * Mathf.Sign(speedDiffx);

        float targetSpeedy = moveDirection.y * MOVESPEED;
        float speedDiffy = targetSpeedy - player2D.velocity.y;
        float acceleratey = (Mathf.Abs(targetSpeedy) > 0.01f) ? ACCEL : DECEL;
        float Roamy = Mathf.Pow(Mathf.Abs(speedDiffy) * acceleratey, 2) * Mathf.Sign(speedDiffy);

        player2D.AddForce(Roamx * Vector2.right);
        player2D.AddForce(Roamy * Vector2.up);

        // Friction
        float frictionx = Mathf.Min(Mathf.Abs(player2D.velocity.x), Mathf.Abs(0.1f));
        frictionx *= Mathf.Sign(player2D.velocity.x);
        player2D.AddForce(Vector2.right * -frictionx, ForceMode2D.Impulse);

        float frictiony = Mathf.Min(Mathf.Abs(player2D.velocity.y), Mathf.Abs(0.1f));
        frictiony *= Mathf.Sign(player2D.velocity.y);
        player2D.AddForce(Vector2.up * -frictiony, ForceMode2D.Impulse);

        mousePosition = Mouse.current.position.ReadValue();
        Debug.Log(mousePosition);
    }

    

    /***********
    * Roam Mode
    ************/

    // Get Vector Value upon Roam keypress
    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    /*************
     * Combat Mode 
     *************/

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 direction = new Vector2(-1 * (Screen.width/2 - mousePosition.x - player2D.position.x), -1 * (Screen.height/2 - mousePosition.y - player2D.position.y));
            direction.Normalize();
            Debug.Log(direction);

            GameObject newEnemy = Instantiate(debug, player2D.position, Quaternion.identity);
            newEnemy.GetComponent<Rigidbody2D>().velocity = direction * 10;
        }
    }
}
