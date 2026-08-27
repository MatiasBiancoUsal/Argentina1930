using UnityEngine;

// Enemigo 2D top-down: patrulla, detecta al jugador (si no est� en sigilo) y lo persigue.
// Al alcanzarlo, reinicia el nivel a trav�s de LevelManager.
// Requiere: Rigidbody2D (gravity scale 0) y opcionalmente un Animator con los mismos
// par�metros que usa Player.cs (MovimientoX, MovimientoY, Caminando).
public class Enemigo : MonoBehaviour
{
    public enum State { Patrol, Chase }

    [Header("Referencias")]
    [Tooltip("Si lo dej�s vac�o, se busca autom�ticamente el objeto con tag 'Player'.")]
    [SerializeField] private Transform jugador;
    [Tooltip("El script Player.cs del jugador, para leer 'enSigilo'. Se autocompleta si dej�s 'Jugador' vac�o.")]
    [SerializeField] private Player playerScript;

    [Header("Detecci�n")]
    [SerializeField] private float rangoVision = 5f;
    [Tooltip("Capas que bloquean la l�nea de visi�n (paredes). Dejar en 'Nothing' para ignorar obst�culos.")]
    [SerializeField] private LayerMask capaObstaculos;
    [SerializeField] private float distanciaMuerte = 0.5f;

    [Header("Velocidad")]
    [SerializeField] private float velocidadPatrulla = 1.5f;
    [SerializeField] private float velocidadPersecucion = 3.5f;

    [Header("Patrulla (opcional)")]
    [SerializeField] private Transform[] puntosPatrulla;
    [SerializeField] private float tiempoEsperaPatrulla = 2f;

    [Header("Reinicio de nivel")]
    [SerializeField] private float delayReinicio = 1f;

    private Rigidbody2D rb;
    private Animator animator;
    private State estadoActual = State.Patrol;
    private int indicePatrulla = 0;
    private float temporizadorEspera = 0f;
    private bool jugadorAtrapado = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // puede ser null si el enemigo no tiene animaciones

        if (jugador == null)
        {
            GameObject jugadorObj = GameObject.FindGameObjectWithTag("Player");
            if (jugadorObj != null) jugador = jugadorObj.transform;
        }

        if (playerScript == null && jugador != null)
        {
            playerScript = jugador.GetComponent<Player>();
        }
    }

    private void Update()
    {
        if (jugadorAtrapado) return;

        if (estadoActual == State.Patrol)
        {
            ChequearDeteccion();
        }
    }

    private void FixedUpdate()
    {
        if (jugadorAtrapado) return;

        if (estadoActual == State.Patrol)
        {
            Patrullar();
        }
        else if (estadoActual == State.Chase)
        {
            Perseguir();
        }
    }

    private void ChequearDeteccion()
    {
        if (jugador == null || playerScript == null) return;

        // Si el jugador est� en sigilo, no lo detectamos aunque est� cerca.
        if (playerScript.enSigilo) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);
        if (distancia > rangoVision) return;

        // L�nea de visi�n opcional: solo se chequea si asignaste una capa de obst�culos.
        if (capaObstaculos.value != 0)
        {
            Vector2 direccion = ((Vector2)jugador.position - (Vector2)transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direccion, distancia, capaObstaculos);
            if (hit.collider != null) return; // hay una pared tapando al jugador
        }

        estadoActual = State.Chase;
    }

    private void Perseguir()
    {
        if (jugador == null) return;

        Vector2 direccion = ((Vector2)jugador.position - rb.position).normalized;
        rb.MovePosition(rb.position + direccion * velocidadPersecucion * Time.fixedDeltaTime);
        ActualizarAnimator(direccion);

        float distancia = Vector2.Distance(transform.position, jugador.position);
        if (distancia <= distanciaMuerte)
        {
            AtraparJugador();
            return;
        }

        // Si el jugador vuelve a esconderse y se aleja lo suficiente, el enemigo lo pierde.
        // Sac� este bloque si prefer�s que persiga sin soltar una vez que te vio.
        if (playerScript != null && playerScript.enSigilo && distancia > rangoVision)
        {
            estadoActual = State.Patrol;
        }
    }

    private void Patrullar()
    {
        if (puntosPatrulla == null || puntosPatrulla.Length == 0)
        {
            if (animator != null) animator.SetBool("Caminando", false);
            return;
        }

        Transform destino = puntosPatrulla[indicePatrulla];
        float distancia = Vector2.Distance(transform.position, destino.position);

        if (distancia < 0.2f)
        {
            temporizadorEspera += Time.fixedDeltaTime;
            if (animator != null) animator.SetBool("Caminando", false);

            if (temporizadorEspera >= tiempoEsperaPatrulla)
            {
                temporizadorEspera = 0f;
                indicePatrulla = (indicePatrulla + 1) % puntosPatrulla.Length;
            }
        }
        else
        {
            Vector2 direccion = ((Vector2)destino.position - rb.position).normalized;
            rb.MovePosition(rb.position + direccion * velocidadPatrulla * Time.fixedDeltaTime);
            ActualizarAnimator(direccion);
        }
    }

    private void ActualizarAnimator(Vector2 direccion)
    {
        if (animator == null) return;
        animator.SetFloat("MovimientoX", direccion.x);
        animator.SetFloat("MovimientoY", direccion.y);
        animator.SetBool("Caminando", direccion.magnitude > 0.01f);
    }

    private void AtraparJugador()
    {
        if (jugadorAtrapado) return;
        jugadorAtrapado = true;

        Debug.Log("Jugador atrapado. Reiniciando nivel...");

        if (RestartManager.Instance != null)
        {
            RestartManager.Instance.ReiniciarNivel();
        }
        else
        {
            Debug.LogWarning("No se encontró RestartManager en la escena. Creá un GameObject vacío con ese script.");
        }
    }

    // Rango de detecci�n visible en el editor para ajustarlo a ojo.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoVision);
    }
}