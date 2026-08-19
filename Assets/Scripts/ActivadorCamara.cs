using UnityEngine;

public class ActivadorCamara : MonoBehaviour
{
    [SerializeField] private GameObject botonCamara;

    void Start()
    {
        if (InventarioJugador.Instancia != null && InventarioJugador.Instancia.TieneItem("Camara"))
        {
            if (botonCamara != null)
                botonCamara.SetActive(true);
        }
    }

    public void ActivarBoton()
    {
        if (botonCamara != null)
            botonCamara.SetActive(true);
    }



}