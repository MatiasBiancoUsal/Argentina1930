using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// ─────────────────────────────────────────────────────────────────────────────
//  VisorCamara.cs
//  Adjuntar a: el GameObject del Canvas del visor (ej. "Canvas_Visor")
//
//  Qué hace:
//    • Muestra/oculta el panel visor con efecto vintage
//    • Reproduce un flash blanco al sacar foto
//    • Maneja la animación de aparición con curva suave
//
//  Setup en Unity:
//    1. Crear un Canvas en modo "Screen Space - Overlay"
//    2. Dentro del Canvas crear la jerarquía:
//         Canvas_Visor
//           └── PanelVisor          ← Image con sprite de marco/visor vintage
//                 ├── Vineta        ← Image negra circular (Radial fill o sprite)
//                 ├── MarcoFoto     ← Image con el borde estilo cámara
//                 ├── LineaHorizontal ← Image blanca semitransparente (línea central)
//                 ├── LineaVertical   ← Image blanca semitransparente
//                 └── PanelFlash    ← Image blanca, alpha 0 normalmente
//    3. Asignar las referencias en el Inspector
// ─────────────────────────────────────────────────────────────────────────────

public class VisorCamara : MonoBehaviour
{
    // ── Panel principal ──────────────────────────────────────────────────────
    [Header("Panel Visor")]
    [Tooltip("El panel raíz del visor (contiene todo el HUD de la cámara)")]
    public GameObject panelVisor;

    // ── Efecto viñeta y marco ────────────────────────────────────────────────
    [Header("Elementos Visuales Vintage")]
    [Tooltip("Imagen circular oscura en los bordes (viñeta)")]
    public Image vineta;

    [Tooltip("Marco estilo cámara / visor de cuero")]
    public Image marcoFoto;

    [Tooltip("Línea horizontal del visor (crosshair)")]
    public Image lineaHorizontal;

    [Tooltip("Línea vertical del visor (crosshair)")]
    public Image lineaVertical;

    // ── Flash ────────────────────────────────────────────────────────────────
    [Header("Flash al fotografiar")]
    [Tooltip("Panel blanco que hace el efecto flash")]
    public Image panelFlash;

    [Tooltip("Duración del flash en segundos")]
    public float duracionFlash = 0.25f;

    // ── Animación de entrada ─────────────────────────────────────────────────
    [Header("Animación")]
    [Tooltip("Duración de la animación al sacar/guardar la cámara")]
    public float duracionAnimacion = 0.2f;

    // ── Estado interno ───────────────────────────────────────────────────────
    private bool flashEnCurso = false;
    private CanvasGroup canvasGroup;

    // ─────────────────────────────────────────────────────────────────────────

    void Awake()
    {
        // CanvasGroup para fade suave
        canvasGroup = panelVisor != null ? panelVisor.GetComponent<CanvasGroup>() : null;
        if (canvasGroup == null && panelVisor != null)
            canvasGroup = panelVisor.AddComponent<CanvasGroup>();

        // Estado inicial: oculto
        if (panelVisor != null)
            panelVisor.SetActive(false);

        // Flash empieza invisible
        if (panelFlash != null)
            panelFlash.color = new Color(1f, 1f, 1f, 0f);
    }

    // ─────────────────────────────────────────────────────────────────────────

    public void MostrarVisor(bool mostrar)
    {
        if (panelVisor == null) return;

        StopAllCoroutines();

        if (mostrar)
        {
            panelVisor.SetActive(true);
            StartCoroutine(AnimarAlpha(0f, 1f));
        }
        else
        {
            StartCoroutine(OcultarConFade());
        }
    }

    // ─────────────────────────────────────────────────────────────────────────

    public void ReproducirFlash()
    {
        if (!flashEnCurso)
            StartCoroutine(EfectoFlash());
    }

    // ─────────────────────────────────────────────────────────────────────────

    private IEnumerator EfectoFlash()
    {
        flashEnCurso = true;

        if (panelFlash != null)
        {
            // Sube el alpha rápido
            float t = 0f;
            while (t < duracionFlash * 0.3f)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 0.85f, t / (duracionFlash * 0.3f));
                panelFlash.color = new Color(1f, 1f, 1f, alpha);
                yield return null;
            }

            // Baja el alpha más lento
            t = 0f;
            while (t < duracionFlash * 0.7f)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Lerp(0.85f, 0f, t / (duracionFlash * 0.7f));
                panelFlash.color = new Color(1f, 1f, 1f, alpha);
                yield return null;
            }

            panelFlash.color = new Color(1f, 1f, 1f, 0f);
        }

        flashEnCurso = false;
    }

    // ─────────────────────────────────────────────────────────────────────────

    private IEnumerator AnimarAlpha(float desde, float hasta)
    {
        if (canvasGroup == null) yield break;

        float t = 0f;
        canvasGroup.alpha = desde;

        while (t < duracionAnimacion)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(desde, hasta, t / duracionAnimacion);
            yield return null;
        }

        canvasGroup.alpha = hasta;
    }

    private IEnumerator OcultarConFade()
    {
        yield return StartCoroutine(AnimarAlpha(1f, 0f));

        if (panelVisor != null)
            panelVisor.SetActive(false);
    }
}
