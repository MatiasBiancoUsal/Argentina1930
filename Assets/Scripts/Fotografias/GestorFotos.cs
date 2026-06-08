using System.Collections.Generic;
using UnityEngine;

// ─────────────────────────────────────────────────────────────────────────────
//  GestorFotos.cs
//  Adjuntar a: el mismo GameObject que CamaraFotografica
//
//  Qué hace:
//    • Almacena la lista de Texture2D de las fotos tomadas
//    • Notifica a LibretaPistas para que actualice la sección "Fotografías reveladas"
//    • Evento estático para que cualquier script pueda reaccionar a una foto nueva
// ─────────────────────────────────────────────────────────────────────────────

public class GestorFotos : MonoBehaviour
{
    // ── Evento ───────────────────────────────────────────────────────────────
    // Otros scripts pueden suscribirse: GestorFotos.OnFotoTomada += MiMetodo;
    public static event System.Action<Texture2D> OnFotoTomada;

    // ── Referencias ──────────────────────────────────────────────────────────
    [Header("Referencias")]
    [Tooltip("La LibretaPistas de la escena. Se busca automáticamente si queda vacío.")]
    public LibretaPistas libretaPistas;

    // ── Lista de fotos ───────────────────────────────────────────────────────
    private List<Texture2D> fotos = new List<Texture2D>();

    // ── Getter público ───────────────────────────────────────────────────────
    public List<Texture2D> Fotos => fotos;
    public int CantidadFotos => fotos.Count;

    // ─────────────────────────────────────────────────────────────────────────

    void Awake()
    {
        if (libretaPistas == null)
            libretaPistas = FindFirstObjectByType<LibretaPistas>();
    }

    // ─────────────────────────────────────────────────────────────────────────

    public void GuardarFoto(Texture2D foto)
    {
        if (foto == null) return;

        fotos.Add(foto);

        // Notificar a la libreta para que muestre la miniatura
        if (libretaPistas != null)
            libretaPistas.AgregarFoto(foto);

        // Disparar evento para cualquier otro script suscripto
        OnFotoTomada?.Invoke(foto);

        Debug.Log("[GestorFotos] Foto guardada. Total en lista: " + fotos.Count);
    }

    // ─────────────────────────────────────────────────────────────────────────
    // Limpia las texturas al destruir el objeto para no generar memory leaks
    void OnDestroy()
    {
        foreach (var tex in fotos)
        {
            if (tex != null)
                Destroy(tex);
        }
        fotos.Clear();
    }
}
