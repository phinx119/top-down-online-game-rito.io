using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;

    public float health, maxHealth = 20;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    void Start()
    {
        health = maxHealth;
        healthBar.updateHealthBar(health, maxHealth);
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
                takeDamage(bulletDamage, collider.gameObject);
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
        Destroy(gameObject);
        SceneManager.LoadSceneAsync(0);
    }
}
