using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    /************* 
     * Variables 
     *************/

    private GameObject owner;
    public GameObject item;
    private int health;

    // Timer
    private float immuneTimer = 1f;
    private float immuneTimerMax = 1f; // Time before players takes damage again
    private float immuneTimerMob = 1f;
    private float immuneTimerMobMax = .30f;

    // Hit Aprite
    public Sprite mobHit = null;
    public Sprite mobNormal = null;
    public Text textHealth;

    // Update is called once per frame

    void Update()
    {
        immuneTimer += Time.deltaTime;
        this.immuneTimerMob += Time.deltaTime;

        if (this.health <= 0 && this.owner.CompareTag("Enemy"))
        {
            Destroy(owner);
            if (UnityEngine.Random.Range(1, 100) <= 3)
            {
                Instantiate(item, this.owner.GetComponent<Rigidbody2D>().position, Quaternion.identity);
            }
            Destroy(this.owner);
            Spawner.updateKilled();
        }
        else if (this.health <= 0 && this.owner.CompareTag("Player"))
        {
            string curr_scene = SceneManager.GetActiveScene().name;
            SceneManager.UnloadSceneAsync(curr_scene);
            SceneManager.LoadScene("YouDiedScene");
        }

    }

    public void setAttributes(int health, GameObject owner)
    {
        this.health = health;
        this.owner = owner;
    }

    public void heal()
    {
        if (this.owner.CompareTag("Player")) 
        {
            this.health += 30;
            textHealth.text = "Health: " + this.health;
        }
    }

    public void hit(Boolean powerUp)
    {
        if (this.owner.CompareTag("Enemy") && immuneTimerMob > immuneTimerMobMax)
        {
            this.immuneTimerMob = 0;
            this.owner.GetComponent<SpriteRenderer>().sprite = mobHit;
            this.health -= (powerUp) ? 20 : 10;
        }
        else if (this.owner.CompareTag("Player") && immuneTimer > immuneTimerMax)
        {
            immuneTimer = 0;
            this.health -= 10;
            textHealth.text = "Health: " + this.health;
        }
    }
}
