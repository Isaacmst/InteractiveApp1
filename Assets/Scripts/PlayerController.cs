using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;  // Velocidad de movimiento

    private Rigidbody2D rb;   // Referencia al Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obtiene el Rigidbody automáticamente
    }

    void Update()
    {
        // Movimiento con flechas (izquierda/derecha)
        float moveInput = Input.GetAxis("Horizontal");  // -1 para izquierda, 1 para derecha
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);  // Aplica velocidad horizontal
    }

    // Detecta colisión al entrar en contacto
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con una pared o piso, detiene el movimiento horizontal
        if (collision.gameObject.name.Contains("Pared") || collision.gameObject.name.Contains("Piso"))
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);  // Detiene horizontalmente (cambia el estado)
            Debug.Log("¡Colisión detectada! No puedes pasar.");  // Mensaje en consola para probar
        }
    }
}
