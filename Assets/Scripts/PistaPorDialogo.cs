using UnityEngine;

public class PistaPorDialogo : MonoBehaviour
{
    [Header("Sistema de pistas")]
    [TextArea(2, 5)]
    public string pista;

    private bool pistaAgregada = false;

    public void ActivarPista()
    {
        if (!pistaAgregada)
        {
            if (LibretaPistas.Instancia != null)
            {
                LibretaPistas.Instancia.AgregarPista(pista);
                pistaAgregada = true;
            }
            else
            {
                Debug.LogWarning("No existe una LibretaPistas activa.");
            }
        }
    }
}
