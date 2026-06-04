using UnityEngine;

public class Barra : MonoBehaviour
{
    [Header("Configuracion")]
    public float tiempoPreparacion = 3f;

    [Header("Prefab del pedido")]
    public GameObject prefabPedido; // Arrastrá el prefab del sprite acá

    public enum EstadoBarra { Libre, Preparando, PedidoListo }
    public EstadoBarra estado = EstadoBarra.Libre;

    private float timer = 0f;
    private NPCPedido npcActual = null;
    private GameObject pedidoInstancia = null; // El objeto instanciado sobre la barra

    [Header("Indicador Visual (opcional)")]
    public GameObject iconoPedidoListo;

    void Start()
    {
        if (iconoPedidoListo != null)
            iconoPedidoListo.SetActive(false);
    }

    void Update()
    {
        if (estado == EstadoBarra.Preparando)
        {
            timer += Time.deltaTime;
            Debug.Log("[Barra] Preparando... " + timer.ToString("F1") + "s / " + tiempoPreparacion + "s");
            if (timer >= tiempoPreparacion)
            {
                timer = 0f;
                estado = EstadoBarra.PedidoListo;

                // Instanciar el sprite del pedido sobre la barra
                if (prefabPedido != null)
                {
                    pedidoInstancia = Instantiate(prefabPedido, transform.position, Quaternion.identity);
                    Debug.Log("[Barra] Sprite del pedido instanciado.");
                }

                if (npcActual != null)
                    npcActual.SetEstado(NPCPedido.EstadoPedido.PedidoListo);

                if (iconoPedidoListo != null)
                    iconoPedidoListo.SetActive(true);

                Debug.Log("[Barra] Pedido listo para recoger!");
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
            Debug.Log("[Barra] Pedido recibido. Preparando en " + tiempoPreparacion + " segundos...");
            return true;
        }
        Debug.Log("[Barra] Está ocupada. Estado actual: " + estado);
        return false;
    }

    // Devuelve el NPC y además pasa la instancia del sprite al jugador
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

            if (iconoPedidoListo != null)
                iconoPedidoListo.SetActive(false);

            Debug.Log("[Barra] Pedido recogido por el jugador.");
            return npc;
        }
        Debug.Log("[Barra] No hay pedido listo. Estado actual: " + estado);
        return null;
    }
}