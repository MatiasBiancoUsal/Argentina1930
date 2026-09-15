using UnityEngine;

public class DialogoPensamiento : MonoBehaviour
{
    public PanelDialogo panelDialogo;

    public string nombrePersonaje;
    public Sprite retratoPersonaje;

    [TextArea(2, 5)]
    public string[] lineasPensamiento;

    private bool yaSeActivo = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !yaSeActivo)
        {
            yaSeActivo = true;

            panelDialogo.IniciarDialogo(
                nombrePersonaje,
                retratoPersonaje,
                lineasPensamiento
            );
        }
    }
}