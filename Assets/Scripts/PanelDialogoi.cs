using TMPro;
using UnityEngine;

public class PanelDialogo : MonoBehaviour
{
    public TextMeshProUGUI textoDialogo;
    public TextMeshProUGUI nombreNPC;

    private string[] lineas;
    private int indice;

    public bool dialogoActivo = false;

    public void IniciarDialogo(string nombre, string[] nuevasLineas)
    {
        nombreNPC.text = nombre;

        lineas = nuevasLineas;
        indice = 0;

        dialogoActivo = true;
        gameObject.SetActive(true);

        textoDialogo.text = lineas[indice];
    }

    void Update()
    {
        if (dialogoActivo && Input.GetKeyDown(KeyCode.E))
        {
            indice++;

            if (indice < lineas.Length)
            {
                textoDialogo.text = lineas[indice];
            }
            else
            {
                dialogoActivo = false;
                gameObject.SetActive(false);
            }
        }
    }
}
