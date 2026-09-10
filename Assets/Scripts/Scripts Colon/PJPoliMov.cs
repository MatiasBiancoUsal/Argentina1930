using UnityEngine;

public class PJPoliMov : MonoBehaviour
{
    public float velocidad = 5f;
    private SpriteRenderer sr;
    private Animator animator;

    private Rigidbody2D rb;
    private float movimientoX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movimientoX = Input.GetAxisRaw("Horizontal");

        if (movimientoX > 0)
        {
            sr.flipX = false; // Mira hacia la derecha
        }
        else if (movimientoX < 0)
        {
            sr.flipX = true; // Mira hacia la izquierda
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movimientoX * velocidad, 0f);
    }
}