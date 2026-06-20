using UnityEngine;

public class NPCDialogos : MonoBehaviour
{
    public PanelDialogo panelDialogo;

    public string nombreNPC;

    [TextArea(2,5)]
    public string[] lineasDialogo;

    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca &&
            Input.GetKeyDown(KeyCode.E) &&
            !panelDialogo.dialogoActivo)
        {
            panelDialogo.IniciarDialogo(nombreNPC, lineasDialogo);
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