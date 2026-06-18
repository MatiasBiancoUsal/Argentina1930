using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GestorFotos))]
public class CamaraFotografica : MonoBehaviour
{
    [Header("Referencias")]
    public VisorCamara visor;
    public GestorFotos gestorFotos;

    [Header("Configuración")]
    public float cooldownFoto = 0.5f;

    private bool modoFotoActivo = false;
    private bool puedeTomarFoto = true;

    private Player playerScript;

    void Awake()
    {
        if (gestorFotos == null)
            gestorFotos = GetComponent<GestorFotos>();

        playerScript = GetComponent<Player>();
    }

    void Update()
    {
        // Activar/desactivar con H
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleModoFoto();
        }

        // Sacar foto
        if (modoFotoActivo && puedeTomarFoto && Input.GetMouseButtonDown(1))
        {
            StartCoroutine(CapturarFoto());
        }
    }

    // Método público para usar desde un botón
    public void ToggleModoFoto()
    {
        modoFotoActivo = !modoFotoActivo;

        if (visor != null)
            visor.MostrarVisor(modoFotoActivo);

        Debug.Log("[Cámara] Modo fotografía: " +
                  (modoFotoActivo ? "ACTIVADO" : "DESACTIVADO"));
    }

    private IEnumerator CapturarFoto()
    {
        puedeTomarFoto = false;

        if (visor != null)
            visor.ReproducirFlash();

        yield return new WaitForEndOfFrame();

        Texture2D foto = ScreenCapture.CaptureScreenshotAsTexture();

        if (gestorFotos != null)
            gestorFotos.GuardarFoto(foto);

        Debug.Log("[Cámara] ¡Foto tomada! Total: " +
                  (gestorFotos != null ? gestorFotos.CantidadFotos : 0));

        yield return new WaitForSeconds(cooldownFoto);
        puedeTomarFoto = true;
    }

    public bool ModoFotoActivo => modoFotoActivo;
}
