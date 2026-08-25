using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Velocidades")]
    public float velocidadNormal = 5f;
    public float velocidadSigilo = 2f;
    public float velocidadSprint = 9f;

    [Header("Estado")]
    public bool enSigilo = false;


    [Header("Agacharse")]
    public bool estaAgachado = false;
    public float velocidadAgachado = 1.5f;

    private BoxCollider2D col;
    private Vector2 colSizeOriginal;
    private Vector2 colOffsetOriginal;


    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Animaciones
    private Animator animator;

    private Vector2 movimiento;
    private float velocidadActual;

    private bool enZonaPasoBajo = false;
    public PanelDialogo panelDialogo;
    public PanelDialogo panelObjeto;


    // Audio

    private AudioSource audioSourcePasos;

    [SerializeField] private AudioClip clipPasos;





    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        col = GetComponent<BoxCollider2D>();
        colSizeOriginal = col.size;
        colOffsetOriginal = col.offset;

    }

    void Start()
    {
        animator.SetFloat("UltimoX", 0f);
        animator.SetFloat("UltimoY", -1f);

        
        audioSourcePasos = GetComponent<AudioSource>();
        audioSourcePasos.clip = clipPasos;


    }

    void Update()
    {

// Bloquear el movimiento cuando este el dialogo activado
   if ((panelDialogo != null && panelDialogo.dialogoActivo) ||
    (panelObjeto != null && panelObjeto.dialogoActivo))
{
    movimiento = Vector2.zero;
    rb.linearVelocity = Vector2.zero;
    animator.SetBool("Caminando", false);
    return;
}

        // Animacion eje Horizontal Y Vertical
        movimiento.x = Input.GetAxisRaw("Horizontal");
        movimiento.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("MovimientoX", movimiento.x);
        animator.SetFloat("MovimientoY", movimiento.y);

        animator.SetBool("Caminando", movimiento.x != 0 || movimiento.y != 0);

        // Animacion Idle
        if (movimiento.x != 0 || movimiento.y != 0)
        {
            animator.SetFloat("UltimoX", movimiento.x);
            animator.SetFloat("UltimoY", movimiento.y);
            
        }

        // Input de movimiento (WASD o flechas)
        // Normalizar para evitar movimiento diagonal m�s r�pido
        if (movimiento.magnitude > 1f)
            movimiento.Normalize();

        // Toggle Sigilo con X
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            enSigilo = !enSigilo;
            Debug.Log("Sigilo: " + (enSigilo ? "ACTIVADO" : "DESACTIVADO"));

            // Feedback visual: tinte azul en sigilo // Se pone como transparente
            if (sr != null)
                sr.color = enSigilo ? new Color(0.5f, 0.7f, 1f, 0.75f) : Color.white;
        }




        // Sonido de pasos
        bool seEstaMoviendo = movimiento.magnitude > 0.1f;
        // Debug.Log("Moviendose: " + seEstaMoviendo + " | Sigilo: " + enSigilo + " | Playing: " + audioSourcePasos.isPlaying);

        if (seEstaMoviendo && !enSigilo)
        {
            if (!audioSourcePasos.isPlaying)
                audioSourcePasos.Play();
        }
        else
        {
            if (audioSourcePasos.isPlaying)
                audioSourcePasos.Stop();
        }




        // Toggle agacharse con C
        if (Input.GetKeyDown(KeyCode.C))
        {
            estaAgachado = true;
            col.size = new Vector2(colSizeOriginal.x, colSizeOriginal.y * 0.5f);
            col.offset = new Vector2(colOffsetOriginal.x, colOffsetOriginal.y - colSizeOriginal.y * 0.25f);
            animator.SetBool("Agachado", true);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            estaAgachado = false;
            col.size = colSizeOriginal;
            col.offset = colOffsetOriginal;
            animator.SetBool("Agachado", false);
        }
        if (Input.GetKeyDown(KeyCode.C) && enZonaPasoBajo)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

        // Prioridad de velocidad

        if (estaAgachado)
        {
            velocidadActual = velocidadAgachado;
        }
        else if (enSigilo)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ZonaPasoBajo>() != null)
        {
            enZonaPasoBajo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<ZonaPasoBajo>() != null)
        {
            enZonaPasoBajo = false;
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }



  void FixedUpdate()
{
  if ((panelDialogo != null && panelDialogo.dialogoActivo) ||
    (panelObjeto != null && panelObjeto.dialogoActivo))
{
    return;
}
    rb.MovePosition(rb.position + movimiento * velocidadActual * Time.fixedDeltaTime);
}
}