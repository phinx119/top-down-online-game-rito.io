using UnityEngine;

public class PlayerEnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;

    public float shootInterval = 0.2f;
    public float bulletForce;
    public float detectionRange = 10f;

    private float shootIntervalCounter;

    void Update()
    {
        shootIntervalCounter -= Time.deltaTime;

        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null && IsEnemyInRange(nearestEnemy.transform))
        {
            RotateGun(nearestEnemy.transform);
            if (shootIntervalCounter <= 0)
            {
                FireBullet();
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = player;
            }
        }

        return nearestEnemy;
    }

    bool IsEnemyInRange(Transform enemy)
    {
        float distance = Vector3.Distance(transform.position, enemy.position);
        return distance <= detectionRange;
    }

    void RotateGun(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void FireBullet()
    {
        shootIntervalCounter = shootInterval;

        GameObject bulletTmp = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}