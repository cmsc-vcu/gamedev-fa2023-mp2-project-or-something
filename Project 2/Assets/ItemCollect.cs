using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Item go boom when 'Player' collected the item
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.GetComponent<HealthSystem>().heal();
        }
    }
}
