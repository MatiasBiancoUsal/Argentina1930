using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Velocidades")]
    public float velocidadNormal = 5f;
    public float velocidadSigilo = 2f;
    public float velocidadSprint = 9f;

    [Header("Estado")]
    public bool enSigilo = false;

    private Rigidbody2D rb;
    private Vector2 movimiento;
    private float velocidadActual;

    // Conectar SpriteRenderer
    private SpriteRenderer sr;

    // Animaciones
    private float movimientoX;
    private float movimientoY;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Animacion eje Horizontal
        movimientoX = Input.GetAxisRaw("Horizontal");
        movimientoY = Input.GetAxisRaw("Vertical");
        animator.SetFloat("MovimientoX", movimientoX);
        animator.SetFloat("MovimientoY", movimientoY);

        // Input de movimiento (WASD o flechas) 
        movimiento.x = Input.GetAxisRaw("Horizontal"); // A/D o flechas
        movimiento.y = Input.GetAxisRaw("Vertical");   // W/S o flechas

        // Normalizar para evitar movimiento diagonal más rápido
        if (movimiento.magnitude > 1f)
            movimiento.Normalize();

        // Toggle Sigilo con X
        if (Input.GetKeyDown(KeyCode.X))
        {
            enSigilo = !enSigilo;
            Debug.Log("Sigilo: " + (enSigilo ? "ACTIVADO" : "DESACTIVADO"));

            // Feedback visual opcional: tinte azul en sigilo // Se pone como transparente
            if (sr != null)
                sr.color = enSigilo ? new Color(0.5f, 0.7f, 1f, 0.75f) : Color.white;
        }

        // Prioridad de velocidad 
        // Sigilo tiene prioridad sobre sprint (no tiene sentido correr en sigilo)
        if (enSigilo)
        {
            velocidadActual = velocidadSigilo;
        }
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            velocidadActual = velocidadSprint;
        }
        else
        {
            velocidadActual = velocidadNormal;
        }
    }

    void FixedUpdate()
    {
        // Mover usando Rigidbody2D para respetar la física y los colliders
        rb.MovePosition(rb.position + movimiento * velocidadActual * Time.fixedDeltaTime);
    }
}