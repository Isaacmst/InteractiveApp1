using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Rigidbody2D rb;
    private Animator anim;
    private float baseScaleX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        baseScaleX = transform.localScale.x;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        Debug.Log("Move Input: " + moveInput); // Depuración
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        float speedValue = Mathf.Abs(moveInput);
        anim.SetFloat("speed", speedValue);
        float direction = moveInput > 0 ? 1 : (moveInput < 0 ? -1 : transform.localScale.x / Mathf.Abs(transform.localScale.x));
        transform.localScale = new Vector3(baseScaleX * direction, transform.localScale.y, transform.localScale.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed, calling Shoot()");
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
}