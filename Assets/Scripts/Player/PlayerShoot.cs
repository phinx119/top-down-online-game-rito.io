using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;

    public float TimeBtwFire = 0.2f;
    public float bulletForce;
    public float bulletLifeTime = 2f;
    public int poolSize = 20;

    private float timeBtwFireCounter;
    private ObjectPool<MonoBehaviour> bulletPool;

    void Start()
    {
        MonoBehaviour bulletComponent = bullet.GetComponent<MonoBehaviour>();
        bulletPool = new ObjectPool<MonoBehaviour>(bulletComponent, poolSize);

        timeBtwFireCounter = TimeBtwFire;
    }

    void Update()
    {
        RotateGun();
        timeBtwFireCounter -= Time.deltaTime;

        if (Input.GetMouseButton(0) && timeBtwFireCounter < 0)
        {
            FireBullet();
        }
    }

    void RotateGun()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
    }

    void FireBullet()
    {
        timeBtwFireCounter = TimeBtwFire;

        MonoBehaviour bulletTmp = bulletPool.GetPooledObject();
        if (bulletTmp != null)
        {
            bulletTmp.transform.position = firePoint.position;
            bulletTmp.transform.rotation = firePoint.rotation;
            bulletTmp.gameObject.SetActive(true);

            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero; // Reset velocity
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

            StartCoroutine(DeactivateBulletAfterTime(bulletTmp.gameObject, bulletLifeTime));
        }
        else
        {
            // Optionally: Instantiate a new bullet if pool is empty (not recommended for pooling)
            GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

            StartCoroutine(DeactivateBulletAfterTime(newBullet, bulletLifeTime));
        }
    }

    IEnumerator DeactivateBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        bullet.SetActive(false);
    }
}
