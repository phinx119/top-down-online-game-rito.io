using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    private GameObject player;

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
        if (player != null)
        {
            player.GetComponent<PlayerStats>().currHealth = health;
            player.GetComponent<PlayerStats>().maxHealth = maxHealth;
        }
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
        Debug.Log("Player died.");
        Destroy(bullet);

        Destroy(gameObject);

        SceneManager.LoadSceneAsync(0);
    }
}
