using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction = Vector2.right;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}