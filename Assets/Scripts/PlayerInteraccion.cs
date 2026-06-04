using UnityEngine;

public class PlayerInteraccion : MonoBehaviour
{
    private NPCPedido npcEnRango = null;
    private Barra barraEnRango = null;
    private NPCPedido pedidoEnMano = null;
    private GameObject spritePedido = null; // El sprite que sigue al jugador

    [Header("Posicion del pedido sobre el personaje")]
    public Vector3 offsetPedido = new Vector3(0.3f, 0.5f, 0f); // Ajustá desde el Inspector

    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Interactuar();
    }

    void Interactuar()
    {
        Debug.Log("[Player] Espacio presionado. " +
                  "En rango NPC: " + (npcEnRango != null ? npcEnRango.gameObject.name : "ninguno") +
                  " | En rango Barra: " + (barraEnRango != null ? "sí" : "no") +
                  " | Lleva pedido: " + (pedidoEnMano != null ? "sí" : "no"));

        // CASO 1: Lleva pedido + está en NPC → entregar pedido
        if (pedidoEnMano != null && npcEnRango != null)
        {
            Debug.Log("[Player] Intentando entregar pedido al NPC...");
            if (npcEnRango.IntentarEntregarPedido())
            {
                pedidoEnMano = null;
                gameManager.AgregarMoneda();

                // Destruir el sprite del pedido
                if (spritePedido != null)
                {
                    Destroy(spritePedido);
                    spritePedido = null;
                    Debug.Log("[Player] Sprite del pedido destruido.");
                }
            }
            return;
        }

        // CASO 2: Lleva pedido + está en barra → dejar pedido
        if (pedidoEnMano != null && barraEnRango != null)
        {
            Debug.Log("[Player] Intentando dejar pedido en la barra...");
            if (barraEnRango.IntentarDejarPedido(pedidoEnMano))
                pedidoEnMano = null;
            return;
        }

        // CASO 3: No lleva nada + está en NPC → tomar pedido
        if (pedidoEnMano == null && npcEnRango != null)
        {
            Debug.Log("[Player] Intentando tomar pedido del NPC...");
            if (npcEnRango.IntentarTomarPedido())
                pedidoEnMano = npcEnRango;
            return;
        }

        // CASO 4: No lleva nada + está en barra → recoger pedido listo
        if (pedidoEnMano == null && barraEnRango != null)
        {
            Debug.Log("[Player] Intentando recoger pedido de la barra...");
            GameObject instancia;
            NPCPedido npc = barraEnRango.IntentarRecogerPedido(out instancia);
            if (npc != null)
            {
                pedidoEnMano = npc;

                // Hacer el sprite hijo del personaje
                if (instancia != null)
                {
                    spritePedido = instancia;
                    spritePedido.transform.SetParent(transform);
                    spritePedido.transform.localPosition = offsetPedido;
                    Debug.Log("[Player] Sprite del pedido ahora sigue al jugador.");
                }

                Debug.Log("[Player] Pedido recogido de la barra. Llevarlo al NPC.");
            }
            return;
        }

        Debug.Log("[Player] No hay nada con qué interactuar.");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out NPCPedido npc))
        {
            npcEnRango = npc;
            Debug.Log("[Player] Entró en rango del NPC: " + npc.gameObject.name);
        }
        if (other.TryGetComponent(out Barra barra))
        {
            barraEnRango = barra;
            Debug.Log("[Player] Entró en rango de la Barra.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out NPCPedido npc) && npcEnRango == npc)
        {
            npcEnRango = null;
            Debug.Log("[Player] Salió del rango del NPC: " + npc.gameObject.name);
        }
        if (other.TryGetComponent(out Barra barra) && barraEnRango == barra)
        {
            barraEnRango = null;
            Debug.Log("[Player] Salió del rango de la Barra.");
        }
    }

    public bool LlevaPedido() => pedidoEnMano != null;
}