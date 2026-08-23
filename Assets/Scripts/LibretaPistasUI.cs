using TMPro;
using UnityEngine;

public class LibretaPistasUI : MonoBehaviour
{
    [Header("Panel Libreta")]
    public GameObject panelLibreta;
    public TMP_Text textoPistas;
    public GameObject iconoNuevaPista;

    [Header("Fotografias")]
    public GameObject panelFotografias;
    public Transform gridFotos;
    public GameObject prefabMiniatura;
    public TMP_Text textoContadorFotos;

    private bool mostrandoFotos = false;

    private void Start()
    {
        panelLibreta.SetActive(false);

        if (iconoNuevaPista != null)
            iconoNuevaPista.SetActive(false);

        if (panelFotografias != null)
            panelFotografias.SetActive(false);

        ActualizarInterfaz();
    }

    public void AbrirCerrarLibreta()
    {
        bool abrir = !panelLibreta.activeSelf;

        panelLibreta.SetActive(abrir);

        if (abrir)
        {
            if (iconoNuevaPista != null)
                iconoNuevaPista.SetActive(false);

            MostrarPestanaPistas();
            ActualizarInterfaz();
        }
    }

    public void MostrarPestanaPistas()
    {
        mostrandoFotos = false;

        if (textoPistas != null)
            textoPistas.gameObject.SetActive(true);

        if (panelFotografias != null)
            panelFotografias.SetActive(false);

        ActualizarInterfaz();
    }

    public void MostrarPestanaFotos()
    {
        mostrandoFotos = true;

        if (textoPistas != null)
            textoPistas.gameObject.SetActive(false);

        if (panelFotografias != null)
            panelFotografias.SetActive(true);
    }

    public void ToggleFotos()
    {
        if (mostrandoFotos)
            MostrarPestanaPistas();
        else
            MostrarPestanaFotos();
    }

    private void ActualizarInterfaz()
    {
        if (LibretaPistas.Instancia == null)
        {
            Debug.LogWarning("No existe una LibretaPistas en la escena.");
            return;
        }

        textoPistas.text = "";

        foreach (string pista in LibretaPistas.Instancia.ObtenerPistas())
        {
            textoPistas.text += "• " + pista + "\n\n";
        }

        if (textoContadorFotos != null)
        {
            textoContadorFotos.text =
                "Fotografias: " +
                LibretaPistas.Instancia.ObtenerFotos().Count;
        }
    }

    public void NuevaPista()
{
    if (iconoNuevaPista != null)
        iconoNuevaPista.SetActive(true);
}

}