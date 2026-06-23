using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelDialogo : MonoBehaviour
{
    public TextMeshProUGUI textoDialogo;
    public TextMeshProUGUI nombreNPC;
    public Image retratoNPC;

    private string[] lineas;
    private int indice;

    public bool dialogoActivo = false;

    public void IniciarDialogo(string nombre, Sprite retrato, string[] nuevasLineas)
    {
        nombreNPC.text = nombre;
        retratoNPC.sprite = retrato;
        retratoNPC.preserveAspect = true;
        retratoNPC.SetNativeSize();

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