using UnityEngine;

public class NPCDialogos : MonoBehaviour
{
    public PanelDialogo panelDialogo;

    public string nombreNPC;

    public Sprite retratoNPC;

    public GameObject indicador;
    private bool yaHablo = false;

    [TextArea(2,5)]
    public string[] lineasDialogo;

    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca &&
            Input.GetKeyDown(KeyCode.E) &&
            !panelDialogo.dialogoActivo)
        {
            panelDialogo.IniciarDialogo(nombreNPC, retratoNPC, lineasDialogo);
            yaHablo = true;

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