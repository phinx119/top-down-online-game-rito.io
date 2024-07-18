using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject player;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = player.GetComponent<PlayerStats>().speed;
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        // Movement
        if (movement.x == 0 || movement.y == 0)
        {
            rb.velocity = movement * moveSpeed;
        }
        else
        {
            rb.velocity = movement * (moveSpeed / squareRoot(2));
        }
    }

    static float squareRoot(int number)
    {
        float temp;
        float sr = number / 2;

        do
        {
            temp = sr;
            sr = (temp + (number / temp)) / 2;
        } while ((temp - sr) != 0);
        return sr;
    }
}