using UnityEngine;
using TMPro;

public class TiendaMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textoMonedas;

    // ANALYTICS: si compró algo mientras la tienda estuvo abierta
    private bool compro = false;

    void OnEnable()
    {
        ActualizarMonedas();

        // ANALYTICS: shop_open
        compro = false;
        int dinero = GameManager.Instancia != null ? GameManager.Instancia.ObtenerMonedas() : 0;
        AnalyticsManager.Instance?.ShopOpen(dinero);
    }

    void OnDisable()
    {
        // ANALYTICS: shop_close
        AnalyticsManager.Instance?.ShopClose(compro);
    }

    void ActualizarMonedas()
    {
        if (textoMonedas != null)
            textoMonedas.text = "Monedas: " + GameManager.Instancia?.ObtenerMonedas();
    }

    public void ComprarTicket()
    {
        Comprar("Ticket", 1);
    }

    public void ComprarCamara()
    {
        Comprar("Camara", 1);
    }

    private void Comprar(string item, int precio)
    {
        if (GameManager.Instancia.GastarMonedas(precio))
        {
            InventarioJugador.Instancia.AgregarItem(item);
            ActualizarMonedas();
            Debug.Log(item + " comprado");

            // ANALYTICS: item_purchase
            compro = true;
            AnalyticsManager.Instance?.ItemPurchase(item);
        }
        else
        {
            Debug.Log("No tenés suficientes monedas");

            // ANALYTICS: not_enough_money
            int faltante = precio - GameManager.Instancia.ObtenerMonedas();
            AnalyticsManager.Instance?.NotEnoughMoney(faltante);
        }
    }
}
