using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LibretaPistas : MonoBehaviour
{
    [Header("Panel Libreta")]
    public GameObject panelLibreta;
    public TMP_Text textoPistas;
    public GameObject iconoNuevaPista;

    [Header("Fotografias Reveladas")]
    public GameObject panelFotografias;
    public Transform gridFotos;
    public GameObject prefabMiniatura;
    public TMP_Text textoContadorFotos;

    private List<string> pistas = new List<string>();
    private List<Texture2D> fotos = new List<Texture2D>();
    private bool mostrandoFotos = false;

    void Start()
    {
        panelLibreta.SetActive(false);

        if (iconoNuevaPista != null)
            iconoNuevaPista.SetActive(false);

        if (panelFotografias != null)
            panelFotografias.SetActive(false);

        ActualizarContadorFotos();
    }

    public void AbrirCerrarLibreta()
    {
        bool abrir = !panelLibreta.activeSelf;
        panelLibreta.SetActive(abrir);

        if (abrir && iconoNuevaPista != null)
            iconoNuevaPista.SetActive(false);

        if (abrir)
            MostrarPestanaPistas();
    }

    public void AgregarPista(string nuevaPista)
    {
        if (!pistas.Contains(nuevaPista))
        {
            pistas.Add(nuevaPista);
            ActualizarTexto();

            if (iconoNuevaPista != null)
                iconoNuevaPista.SetActive(true);
        }
    }

    void ActualizarTexto()
    {
        textoPistas.text = "";
        foreach (string pista in pistas)
            textoPistas.text += "• " + pista + "\n\n";
    }

    public void AgregarFoto(Texture2D foto)
    {
        if (foto == null) return;

        fotos.Add(foto);

        if (prefabMiniatura != null && gridFotos != null)
        {
            GameObject miniatura = Instantiate(prefabMiniatura, gridFotos);
            RawImage rawImg = miniatura.GetComponent<RawImage>();
            if (rawImg != null)
                rawImg.texture = foto;
        }

        ActualizarContadorFotos();

        if (iconoNuevaPista != null)
            iconoNuevaPista.SetActive(true);
    }

    public void ToggleFotos()
    {
        mostrandoFotos = !mostrandoFotos;

        if (mostrandoFotos)
            MostrarPestanaFotos();
        else
            MostrarPestanaPistas();
    }

    public void MostrarPestanaPistas()
    {
        mostrandoFotos = false;

        if (textoPistas != null)
            textoPistas.gameObject.SetActive(true);

        if (panelFotografias != null)
            panelFotografias.SetActive(false);
    }

    public void MostrarPestanaFotos()
    {
        mostrandoFotos = true;

        if (textoPistas != null)
            textoPistas.gameObject.SetActive(false);

        if (panelFotografias != null)
            panelFotografias.SetActive(true);
    }

    private void ActualizarContadorFotos()
    {
        if (textoContadorFotos != null)
            textoContadorFotos.text = "Fotografias: " + fotos.Count;
    }
}