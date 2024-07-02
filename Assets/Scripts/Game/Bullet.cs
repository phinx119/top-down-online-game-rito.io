using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage = 2f;
    public float LifeTime = 2f;

    private CircleCollider2D circleCollider;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        StartCoroutine(EnableColliderAfterDelay(1f));
        Destroy(this.gameObject, LifeTime);
    }

    private IEnumerator EnableColliderAfterDelay(float delay)
    {
        circleCollider.enabled = false;
        yield return new WaitForSeconds(delay);
        circleCollider.enabled = true;
    }
}
