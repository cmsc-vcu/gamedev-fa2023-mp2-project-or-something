using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            if (player.position.x > 39)
            {
                player.position = new Vector2(36, player.position.y);
            }
            else if (player.position.x < -39)
            {
                player.position = new Vector2(-36, player.position.y);
            }
            if (player.position.y > 39)
            {
                player.position = new Vector2(player.position.x, 36);
            }
            else if (player.position.y < -39)
            {
                player.position = new Vector2(player.position.x, -36);
            }
        }
    }
}
