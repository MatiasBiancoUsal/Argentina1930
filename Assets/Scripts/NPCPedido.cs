using UnityEngine;

public class NPCPedido : MonoBehaviour
{
    [Header("Configuracion")]
    public float tiempoEntrePreguntas = 15f;

    public enum EstadoPedido { Esperando, PidioYPendiente, PedidoEnBarra, PedidoListo, Entregado }
    public EstadoPedido estado = EstadoPedido.Esperando;

    [Header("Alerta Visual (opcional)")]
    public GameObject iconoAlerta;

    private float timer = 0f;

    void Start()
    {
        // Asegurarse que el icono empieza apagado
        if (iconoAlerta != null)
            iconoAlerta.SetActive(false);

        SetEstado(EstadoPedido.PidioYPendiente);
        Debug.Log("[NPC " + gameObject.name + "] Arrancó pidiendo.");
    }

    void Update()
    {
        if (estado == EstadoPedido.Entregado)
        {
            timer += Time.deltaTime;
            if (timer >= tiempoEntrePreguntas)
            {
                timer = 0f;
                SetEstado(EstadoPedido.PidioYPendiente);
                Debug.Log("[NPC " + gameObject.name + "] Vuelve a pedir.");
            }
        }
    }

    public void SetEstado(EstadoPedido nuevoEstado)
    {
        estado = nuevoEstado;
        Debug.Log("[NPC " + gameObject.name + "] Estado → " + estado);

        if (iconoAlerta != null)
            iconoAlerta.SetActive(estado == EstadoPedido.PidioYPendiente);
    }

    public bool IntentarTomarPedido()
    {
        if (estado == EstadoPedido.PidioYPendiente)
        {
            SetEstado(EstadoPedido.PedidoEnBarra);
            Debug.Log("[NPC " + gameObject.name + "] Pedido tomado por el jugador.");
            return true;
        }
        Debug.Log("[NPC " + gameObject.name + "] No se puede tomar el pedido. Estado actual: " + estado);
        return false;
    }

    public bool IntentarEntregarPedido()
    {
        if (estado == EstadoPedido.PedidoListo)
        {
            SetEstado(EstadoPedido.Entregado);
            Debug.Log("[NPC " + gameObject.name + "] Pedido entregado. +1 moneda!");
            return true;
        }
        Debug.Log("[NPC " + gameObject.name + "] No se puede entregar. Estado actual: " + estado);
        return false;
    }
}