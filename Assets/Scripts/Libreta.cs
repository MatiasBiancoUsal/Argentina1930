using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LibretaPistas : MonoBehaviour
{
    public GameObject panelLibreta;
    public TMP_Text textoPistas;
    public GameObject iconoNuevaPista;

    private List<string> pistas = new List<string>();

    void Start()
    {
        panelLibreta.SetActive(false);

        if(iconoNuevaPista != null)
            iconoNuevaPista.SetActive(false);
    }

    public void AbrirCerrarLibreta()
    {
        bool abrir = !panelLibreta.activeSelf;

        panelLibreta.SetActive(abrir);

        // Si abre la libreta, desaparece la notificación
        if (abrir && iconoNuevaPista != null)
            iconoNuevaPista.SetActive(false);
    }

    public void AgregarPista(string nuevaPista)
    {
        if (!pistas.Contains(nuevaPista))
        {
            pistas.Add(nuevaPista);

            ActualizarTexto();

            // Aparece la notificación
            if(iconoNuevaPista != null)
                iconoNuevaPista.SetActive(true);
        }
    }

    void ActualizarTexto()
    {
        textoPistas.text = "";

        foreach (string pista in pistas)
        {
            textoPistas.text += "• " + pista + "\n\n";
        }
    }
}