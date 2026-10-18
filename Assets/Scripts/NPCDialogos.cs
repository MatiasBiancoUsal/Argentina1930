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

    [Header("Analytics")]
    [Tooltip("Día del juego en que ocurre esta charla (para dialogue_start_day)")]
    public int dia = 1;
    [Tooltip("Tildalo SOLO en Borges: al terminar esta charla se manda level_complete")]
    public bool terminaNivel = false;

    // true mientras el panel está mostrando el diálogo de ESTE NPC
    private bool hablandoConmigo = false;

    void Update()
    {
        // ANALYTICS: detecta cuando se cerró el diálogo de este NPC
        if (hablandoConmigo && !panelDialogo.dialogoActivo)
        {
            hablandoConmigo = false;

            // dialogue_skipped + npc_talk_count
            AnalyticsManager.Instance?.DialogueEnd(nombreNPC, false);

            // level_complete (solo Borges)
            if (terminaNivel)
                AnalyticsManager.Instance?.LevelComplete();
        }

        if (jugadorCerca &&
            Input.GetKeyDown(KeyCode.E) &&
            !panelDialogo.dialogoActivo)
        {
            panelDialogo.IniciarDialogo(nombreNPC, retratoNPC, lineasDialogo);
            yaHablo = true;

            // ANALYTICS: dialogue_start + dialogue_start_day
            hablandoConmigo = true;
            AnalyticsManager.Instance?.DialogueStart(nombreNPC, dia);

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
