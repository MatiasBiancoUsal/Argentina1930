using UnityEngine;

public class PlayerInteraccionJoaco : MonoBehaviour
{
    private NPCPedido npcEnRango = null;
    private Barra barraEnRango = null;
    private NPCPedido pedidoEnMano = null;
    private GameObject spritePedido = null;

    [Header("Posicion del pedido sobre el personaje")]
    public Vector3 offsetPedido = new Vector3(0.3f, 0.5f, 0f);
    public bool Estrella = true;

    private GameManager gameManager;

    private Animator animator;
    [SerializeField] public GameObject estrella;
    [SerializeField] private Interactuar animacionInteraccion;
    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        animator = GetComponent<Animator>();
        

    }

    private void Start()
    {
        estrella.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
            Interactuar();
        

    }

    void Interactuar()
    {
        // CASO 1: Lleva pedido + está en barra → dejar pedido
        if (pedidoEnMano != null && barraEnRango != null)
        {
            if (barraEnRango.IntentarDejarPedido(pedidoEnMano))
            {
                pedidoEnMano = null;

                animacionInteraccion.ActivarAnimacion();
            }

            return;
        }

        // CASO 2: Lleva pedido + está en NPC → entregar
        if (pedidoEnMano != null && npcEnRango != null)
        {
            if (npcEnRango.IntentarEntregarPedido())
            {
                if (spritePedido != null)
                {
                    Destroy(spritePedido);
                    spritePedido = null;
                }

                pedidoEnMano = null;
                gameManager.AgregarMoneda();

                animacionInteraccion.ActivarAnimacion();
            }

            return;
        }

        // CASO 3: Sin pedido + en barra con pedido listo → recoger
        if (pedidoEnMano == null && barraEnRango != null)
        {
            GameObject instancia;
            NPCPedido npc = barraEnRango.IntentarRecogerPedido(out instancia);
            if (npc != null)
            {
                pedidoEnMano = npc;
                spritePedido = instancia;
                if (spritePedido != null)
                {
                    spritePedido.transform.SetParent(transform);
                    spritePedido.transform.localPosition = offsetPedido;
                }
                // Tutorial: paso 4 — entregar al cliente
                gameManager.TutorialPedidoRecogido();
            }
            return;
        }

        // CASO 4: Sin pedido + en NPC con pedido pendiente → tomar pedido
        if (pedidoEnMano == null && npcEnRango != null)
        {
            if (npcEnRango.IntentarTomarPedido())
            {
                pedidoEnMano = npcEnRango;
                // Tutorial: paso 2 — llevar a la barra
                gameManager.TutorialPedidoRecibido();
            }
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out NPCPedido npc))
            npcEnRango = npc;

        if (other.TryGetComponent(out Barra barra))
            barraEnRango = barra;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out NPCPedido npc) && npcEnRango == npc)
            npcEnRango = null;

        if (other.TryGetComponent(out Barra barra) && barraEnRango == barra)
            barraEnRango = null;
    }
}