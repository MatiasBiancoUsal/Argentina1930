using System.Collections;
using UnityEngine;

// ─────────────────────────────────────────────────────────────────────────────
//  CamaraFotografica.cs
//  Adjuntar a: el GameObject del Player (o a un GameObject vacío "CamaraSystem")
//
//  Flujo:
//    H  → toggle modo fotografía (activa/desactiva el visor)
//    Click izquierdo (mientras visor activo) → saca foto → guarda en GestorFotos
// ─────────────────────────────────────────────────────────────────────────────

[RequireComponent(typeof(GestorFotos))]
public class CamaraFotografica : MonoBehaviour
{
    // ── Referencias ──────────────────────────────────────────────────────────
    [Header("Referencias")]
    [Tooltip("El script VisorCamara que maneja el Canvas vintage")]
    public VisorCamara visor;

    [Tooltip("El GestorFotos de la escena (se busca automáticamente si queda vacío)")]
    public GestorFotos gestorFotos;

    // ── Configuración ────────────────────────────────────────────────────────
    [Header("Configuración")]
    [Tooltip("Segundos de bloqueo entre fotos para evitar spam")]
    public float cooldownFoto = 0.5f;

    // ── Estado interno ───────────────────────────────────────────────────────
    private bool modoFotoActivo = false;
    private bool puedeTomarFoto = true;

    // ── Player ref (para bloquear movimiento opcional) ───────────────────────
    private Player playerScript;

    // ─────────────────────────────────────────────────────────────────────────

    void Awake()
    {
        if (gestorFotos == null)
            gestorFotos = GetComponent<GestorFotos>();

        playerScript = GetComponent<Player>();
    }

    void Update()
    {
        // ── Toggle modo foto con H ───────────────────────────────────────────
        if (Input.GetKeyDown(KeyCode.H))
        {
            modoFotoActivo = !modoFotoActivo;

            if (visor != null)
                visor.MostrarVisor(modoFotoActivo);

            Debug.Log("[Cámara] Modo fotografía: " + (modoFotoActivo ? "ACTIVADO" : "DESACTIVADO"));
        }

        // ── Sacar foto con click izquierdo ───────────────────────────────────
        if (modoFotoActivo && puedeTomarFoto && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(CapturarFoto());
        }
    }

    // ─────────────────────────────────────────────────────────────────────────

    private IEnumerator CapturarFoto()
    {
        puedeTomarFoto = false;

        // 1. Efecto flash en el visor
        if (visor != null)
            visor.ReproducirFlash();

        // 2. Esperar al final del frame para que el flash se vea antes de capturar
        yield return new WaitForEndOfFrame();

        // 3. Capturar pantalla como Texture2D
        Texture2D foto = ScreenCapture.CaptureScreenshotAsTexture();

        // 4. Guardar en el gestor (que a su vez notifica a LibretaPistas)
        if (gestorFotos != null)
            gestorFotos.GuardarFoto(foto);

        Debug.Log("[Cámara] ¡Foto tomada! Total: " + (gestorFotos != null ? gestorFotos.CantidadFotos : 0));

        // 5. Cooldown
        yield return new WaitForSeconds(cooldownFoto);
        puedeTomarFoto = true;
    }

    // ── Getter público por si otro script necesita saber si está en modo foto ─
    public bool ModoFotoActivo => modoFotoActivo;
}
