using UnityEngine;
public class NPCDialogos : MonoBehaviour
{
    public PanelDialogo panelDialogo;
    public string nombreNPC;
    public Sprite retratoNPC;
    public PistaPorDialogo pistaPorDialogo;
    public GameObject indicador;
    private bool yaHablo = false;
    [TextArea(2, 5)]
    public string[] lineasDialogo;
    private bool jugadorCerca = false;

    public AudioSource audioSource;      // NUEVO: arrastrá acá el AudioSource
    public AudioClip sonidoInteraccion;  // NUEVO: arrastrá acá el sonido

    void Update()
    {
        if (jugadorCerca &&
            Input.GetKeyDown(KeyCode.E) &&
            !panelDialogo.dialogoActivo)
        {
            panelDialogo.IniciarDialogo(nombreNPC, retratoNPC, lineasDialogo);
            yaHablo = true;

            if (audioSource != null && sonidoInteraccion != null)   // NUEVO
            {
                audioSource.PlayOneShot(sonidoInteraccion);
            }

            if (pistaPorDialogo != null)
            {
                pistaPorDialogo.ActivarPista();
            }
            if (indicador != null)
            {
                indicador.SetActive(false);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = false;
    }
}