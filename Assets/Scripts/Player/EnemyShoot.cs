using UnityEngine;

public class EnemyShoot : MonoBehaviour
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
                shootIntervalCounter = shootInterval;
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= detectionRange)
            {
                return player;
            }
        }
        return null;
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
        GameObject bulletTmp = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}