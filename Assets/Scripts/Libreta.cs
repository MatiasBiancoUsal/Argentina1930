using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LibretaPistas : MonoBehaviour
{
    // ── Lo que ya tenias ─────────────────────────────────────────────────────
    public GameObject panelLibreta;
    public TMP_Text textoPistas;
    public GameObject iconoNuevaPista;

    private List<string> pistas = new List<string>();

    // ── Nuevo: Fotografias reveladas ─────────────────────────────────────────
    public GameObject panelFotografias;
    public Transform gridFotos;
    public GameObject prefabMiniatura;
    public TMP_Text textoContadorFotos;

    private List<Texture2D> fotos = new List<Texture2D>();

    // ────────────────────────────────────────────────────────────────────────

    void Start()
    {
        panelLibreta.SetActive(false);

        if (iconoNuevaPista != null)
            iconoNuevaPista.SetActive(false);

        if (panelFotografias != null)
            panelFotografias.SetActive(false);

        ActualizarContadorFotos();
    }

    // ── Metodos originales (sin cambios) ─────────────────────────────────────

    public void AbrirCerrarLibreta()
    {
        bool abrir = !panelLibreta.activeSelf;
        panelLibreta.SetActive(abrir);

        if (abrir && iconoNuevaPista != null)
            iconoNuevaPista.SetActive(false);
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

    // ── Metodos nuevos: Fotografias ──────────────────────────────────────────

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

    public void MostrarPestanaPistas()
    {
        if (textoPistas != null)
            textoPistas.transform.parent.gameObject.SetActive(true);

        if (panelFotografias != null)
            panelFotografias.SetActive(false);
    }

    public void MostrarPestanaFotos()
    {
        if (textoPistas != null)
            textoPistas.transform.parent.gameObject.SetActive(false);

        if (panelFotografias != null)
            panelFotografias.SetActive(true);
    }

    private void ActualizarContadorFotos()
    {
        if (textoContadorFotos != null)
            textoContadorFotos.text = "Fotografias: " + fotos.Count;
    }
}