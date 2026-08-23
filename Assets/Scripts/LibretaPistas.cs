using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LibretaPistas : MonoBehaviour
{
    public static LibretaPistas Instancia { get; private set; }

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

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;

        DontDestroyOnLoad(gameObject);
    }

    public void AgregarPista(string nuevaPista)
    {
        if (string.IsNullOrEmpty(nuevaPista))
            return;

        if (!pistas.Contains(nuevaPista))
    {
        pistas.Add(nuevaPista);

        Debug.Log("Pista agregada: " + nuevaPista);

        // Avisar a la interfaz de la escena actual
        LibretaPistasUI ui = FindFirstObjectByType<LibretaPistasUI>();

        if (ui != null)
        {
            ui.NuevaPista();
        }
    }
}

    public List<string> ObtenerPistas()
    {
        return pistas;
    }

    public bool TienePista(string pista)
    {
        return pistas.Contains(pista);
    }

    public void AgregarFoto(Texture2D foto)
    {
        if (foto == null)
            return;

        fotos.Add(foto);
    }

    public List<Texture2D> ObtenerFotos()
    {
        return fotos;
    }
}