using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private float baseScaleX;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        baseScaleX = transform.localScale.x;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow);
        bool rightPressed = Input.GetKey(KeyCode.RightArrow);
        Debug.Log("Move Input: " + moveInput + " | Left: " + leftPressed + " | Right: " + rightPressed + " | Player Position: " + transform.position + " | Is Grounded: " + isGrounded);
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        float speedValue = Mathf.Abs(moveInput);
        anim.SetFloat("speed", speedValue);
        float direction = moveInput > 0 ? 1 : (moveInput < 0 ? -1 : transform.localScale.x / Mathf.Abs(transform.localScale.x));
        transform.localScale = new Vector3(baseScaleX * direction, transform.localScale.y, transform.localScale.z);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space detected, calling Shoot()");
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            BulletController bulletScript = bullet.GetComponent<BulletController>();
            bulletScript.direction = new Vector2(transform.localScale.x, 0).normalized;
            Debug.Log("Bala instanciada en: " + firePoint.position);
        }
        else
        {
            Debug.LogWarning("bulletPrefab o firePoint no asignados!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Collision with Ground, isGrounded: " + isGrounded);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("Exited Ground, isGrounded: " + isGrounded);
        }
    }
}