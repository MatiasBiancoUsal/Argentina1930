using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelTutorial;
    public TextMeshProUGUI textoTutorial;

    [Header("Configuracion")]
    public float duracionMensaje = 3f;

    public enum PasoTutorial
    {
        Ninguno,
        // Escena Calle
        Moverse,
        IrAlBar,
        // Escena Bar
        AcercarseAlCliente,
        DejarEnBarra,
        RecogerDeBarra,
        EntregarAlCliente,
        Completo
    }

    private PasoTutorial pasoActual = PasoTutorial.Ninguno;

    public static TutorialManager Instancia { get; private set; }

    void Awake()
    {
        Instancia = this;
        panelTutorial.SetActive(false);
    }

    public void MostrarPaso(PasoTutorial paso)
    {
        if ((int)paso <= (int)pasoActual) return;
        if (pasoActual == PasoTutorial.Completo) return;

        pasoActual = paso;
        string mensaje = ObtenerMensaje(paso);
        if (!string.IsNullOrEmpty(mensaje))
            StartCoroutine(MostrarMensaje(mensaje));
    }

    string ObtenerMensaje(PasoTutorial paso)
    {
        switch (paso)
        {
            case PasoTutorial.Moverse:
                return "Muévete con WASD.";
            case PasoTutorial.IrAlBar:
                return "Ve al bar.";
            case PasoTutorial.AcercarseAlCliente:
                return "¡Un cliente quiere ordenar!\nAcércate y presiona ESPACIO.";
            case PasoTutorial.DejarEnBarra:
                return "Lleva el pedido a la barra\ny presiona ESPACIO para dejarlo.";
            case PasoTutorial.RecogerDeBarra:
                return "¡El pedido está listo!\nAcércate a la barra y presiona ESPACIO.";
            case PasoTutorial.EntregarAlCliente:
                return "Llévale el pedido al cliente\ny presiona ESPACIO para entregarlo.";
            default:
                return "";
        }
    }

    IEnumerator MostrarMensaje(string mensaje)
    {
        textoTutorial.text = mensaje;
        panelTutorial.SetActive(true);

        yield return new WaitForSeconds(duracionMensaje);

        panelTutorial.SetActive(false);
    }
}