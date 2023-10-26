using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public static int fruitCollected;
    public Text text_collected;


    void Awake()
    {
        text_collected.text = "Fruit Collected: 00";
        fruitCollected = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Item go boom when 'Player' collected the item
        if (collision.CompareTag("Player"))
        {
            fruitCollected++;
            Destroy(gameObject);
            text_collected.text = "Fruit Collected: " + fruitCollected.ToString("00");
        }
    }
}
