using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    public GameObject player;

    public float health, maxHealth = 20;
    public bool isPlayerEnemy = false;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
        healthBar.updateHealthBar(health, maxHealth);
    }

    void Update()
    {
        health = player.GetComponent<PlayerStats>().currHealth;
        maxHealth = player.GetComponent<PlayerStats>().maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            Bullet bullet = collider.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                float bulletDamage = bullet.bulletDamage;
                Destroy(collider.gameObject);
                if (!isPlayerEnemy) takeDamage(bulletDamage, collider.gameObject);
            }
        }
    }

    private void SpawnExperienceObjects()
    {
        ExpSpawn expSpawn = FindObjectOfType<ExpSpawn>();
        if (expSpawn != null)
        {
            Debug.Log("Spawning experience objects.");
            expSpawn.SpawnObjectsAtPosition(transform.position, 5);
        }
        else
        {
            Debug.LogError("ExpSpawn is null.");
        }
    }

    void takeDamage(float bulletDamage, GameObject bullet)
    {
        health -= bulletDamage;
        healthBar.updateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            playerDie(bullet);
        }
    }

    void playerDie(GameObject bullet)
    {
        Debug.Log("Player died.");
        Destroy(bullet);
        SpawnExperienceObjects();
        Destroy(gameObject);

        if (gameObject.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
