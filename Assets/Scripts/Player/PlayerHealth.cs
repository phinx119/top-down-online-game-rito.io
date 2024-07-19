using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    public GameObject player;
    public GameObject expSpawnObject;
    private ExpSpawn expSpawn;

    public float health, maxHealth = 20;
    public bool isPlayerEnemy = false;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        player = GameObject.FindGameObjectWithTag("Player");
        expSpawnObject = GameObject.FindGameObjectWithTag("ExpSpawner");
        expSpawn = expSpawnObject.GetComponent<ExpSpawn>();
    }

    void Start()
    {
        health = maxHealth;
        healthBar.updateHealthBar(health, maxHealth);
    }

    void Update()
    {
        player.GetComponent<PlayerStats>().currHealth = health;
        player.GetComponent<PlayerStats>().maxHealth = maxHealth;
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
        Destroy(bullet);
        expSpawn.SpawnExperience(transform.position);

        Destroy(gameObject);

        if (gameObject.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
