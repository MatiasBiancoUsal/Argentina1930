using UnityEngine;

public class Barra : MonoBehaviour
{
    [Header("Preparacion")]
    public float tiempoPreparacion = 3f;
    public GameObject iconoPedidoListo;   // Icono que aparece cuando el pedido está listo
    public GameObject prefabPedido;       // Sprite del pedido que sigue al jugador

    public enum EstadoBarra { Libre, Preparando, PedidoListo }
    public EstadoBarra estado = EstadoBarra.Libre;

    private NPCPedido npcActual = null;
    private float timer = 0f;
    private GameObject pedidoInstancia = null;

    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Start()
    {
        if (iconoPedidoListo != null) iconoPedidoListo.SetActive(false);
    }

    void Update()
    {
        if (estado == EstadoBarra.Preparando)
        {
            timer += Time.deltaTime;
            if (timer >= tiempoPreparacion)
            {
                estado = EstadoBarra.PedidoListo;
                if (iconoPedidoListo != null) iconoPedidoListo.SetActive(true);

                // Instanciar sprite del pedido en la barra
                if (prefabPedido != null)
                    pedidoInstancia = Instantiate(prefabPedido, transform.position, Quaternion.identity);

                // Tutorial paso 3 — recoger de la barra
                gameManager?.TutorialPedidoListo();

                Debug.Log("[Barra] Pedido listo.");
            }
        }
    }

    public bool IntentarDejarPedido(NPCPedido npc)
    {
        if (estado == EstadoBarra.Libre)
        {
            estado = EstadoBarra.Preparando;
            npcActual = npc;
            timer = 0f;
            Debug.Log("[Barra] Preparando pedido...");
            return true;
        }
        return false;
    }

    public NPCPedido IntentarRecogerPedido(out GameObject instancia)
    {
        instancia = null;
        if (estado == EstadoBarra.PedidoListo)
        {
            NPCPedido npc = npcActual;
            instancia = pedidoInstancia;
            pedidoInstancia = null;
            estado = EstadoBarra.Libre;
            npcActual = null;

            if (iconoPedidoListo != null) iconoPedidoListo.SetActive(false);

            Debug.Log("[Barra] Pedido recogido.");
            return npc;
        }
        return null;
    }
}