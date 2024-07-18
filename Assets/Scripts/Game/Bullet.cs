using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage = 2f;
    public float LifeTime = 1.5f;

    private CircleCollider2D circleCollider;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        circleCollider = GetComponent<CircleCollider2D>();
        StartCoroutine(EnableColliderAfterDelay(0.2f));
        Destroy(this.gameObject, LifeTime);
    }

    void Update()
    {
        bulletDamage = player.GetComponent<PlayerStats>().damage;
    }

    private IEnumerator EnableColliderAfterDelay(float delay)
    {
        circleCollider.enabled = false;
        yield return new WaitForSeconds(delay);
        circleCollider.enabled = true;
    }
}
