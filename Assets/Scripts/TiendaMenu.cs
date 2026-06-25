using UnityEngine;
using TMPro;

public class TiendaMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textoMonedas;

    void OnEnable()
    {
        ActualizarMonedas();
    }

    void ActualizarMonedas()
    {
        if (textoMonedas != null)
            textoMonedas.text = "Monedas: " + GameManager.Instancia?.ObtenerMonedas();
    }

    public void ComprarTicket()
    {
        int precio = 1;
        if (GameManager.Instancia.GastarMonedas(precio))
        {
            InventarioJugador.Instancia.AgregarItem("Ticket");
            ActualizarMonedas();
            Debug.Log("Ticket comprado");
        }
        else
        {
            Debug.Log("No tenés suficientes monedas");
        }
    }
}