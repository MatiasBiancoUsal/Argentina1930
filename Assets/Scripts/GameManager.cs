using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Monedas")]
    public TextMeshProUGUI textoMonedas;

    private int monedas = 0;

    void Start()
    {
        ActualizarUI();
    }

    public void AgregarMoneda()
    {
        monedas++;
        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (textoMonedas != null)
            textoMonedas.text = "Monedas: " + monedas;
    }

    // --- Tutorial: disparadores ---

    public void TutorialNPCActivo()
    {
        TutorialManager.Instancia?.MostrarPaso(TutorialManager.PasoTutorial.AcercarseAlCliente);
    }

    public void TutorialPedidoRecibido()
    {
        TutorialManager.Instancia?.MostrarPaso(TutorialManager.PasoTutorial.DejarEnBarra);
    }

    public void TutorialPedidoListo()
    {
        TutorialManager.Instancia?.MostrarPaso(TutorialManager.PasoTutorial.RecogerDeBarra);
    }

    public void TutorialPedidoRecogido()
    {
        TutorialManager.Instancia?.MostrarPaso(TutorialManager.PasoTutorial.EntregarAlCliente);
    }
}