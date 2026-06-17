using UnityEngine;

public class NPCPedido : MonoBehaviour
{
    [Header("Icono de alerta")]
    public GameObject iconoAlerta;   // El "!" sobre el NPC

    public enum EstadoNPC { Esperando, PedidoActivo, Satisfecho }
    public EstadoNPC estado = EstadoNPC.Esperando;

    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Start()
    {
        if (iconoAlerta != null) iconoAlerta.SetActive(false);
        // Activar pedido al inicio (podés controlarlo desde afuera si preferís)
        ActivarPedido();
    }

    public void ActivarPedido()
    {
        if (estado != EstadoNPC.Esperando) return;
        estado = EstadoNPC.PedidoActivo;
        if (iconoAlerta != null) iconoAlerta.SetActive(true);

        // Tutorial paso 1 — acercarse al cliente
        gameManager?.TutorialNPCActivo();
    }

    // El jugador recoge el pedido del NPC
    public bool IntentarTomarPedido()
    {
        if (estado != EstadoNPC.PedidoActivo) return false;
        if (iconoAlerta != null) iconoAlerta.SetActive(false);
        return true;
    }

    // El jugador entrega el pedido terminado
    public bool IntentarEntregarPedido()
    {
        if (estado != EstadoNPC.PedidoActivo) return false;
        estado = EstadoNPC.Satisfecho;
        Debug.Log("[NPC] Pedido entregado. ¡Gracias!");
        return true;
    }
}