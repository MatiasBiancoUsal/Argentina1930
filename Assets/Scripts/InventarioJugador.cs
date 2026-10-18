using UnityEngine;
using System.Collections.Generic;

public class InventarioJugador : MonoBehaviour
{
    public static InventarioJugador Instancia { get; private set; }

    [Header("UI Inventario")]
    [SerializeField] private GameObject slotTicket;

    private List<string> items = new List<string>();

    void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AgregarItem(string item)
    {
        items.Add(item);

        if (item == "Ticket" && slotTicket != null)
            slotTicket.SetActive(true);

        if (item == "Camara")
        {
            ActivadorCamara activador = FindFirstObjectByType<ActivadorCamara>();
            if (activador != null)
                activador.ActivarBoton();

            // ANALYTICS: camera_obtained
            AnalyticsManager.Instance?.CameraObtained();
        }

        Debug.Log("Inventario: " + string.Join(", ", items));
    }

    public bool TieneItem(string item)
    {
        return items.Contains(item);
    }

    public List<string> ObtenerItems()
    {
        return items;
    }
}