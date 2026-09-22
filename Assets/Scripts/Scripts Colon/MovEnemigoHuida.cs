using UnityEngine;

public class MovEnemigoHuida : MonoBehaviour
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
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        animator.SetBool("camina", false);

        if (movimientoX > 0)
        {
            sr.flipX = false; // Mira hacia la derecha
            animator.SetBool("camina", true);
        }
        else if (movimientoX < 0)
        {
            sr.flipX = true; // Mira hacia la izquierda
            animator.SetBool("camina", true);
        }
        else if (movimientoX == 0)
        {
            animator.SetBool("camina", false);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movimientoX * velocidad, 0f);
    }
}