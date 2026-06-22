using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }

    [Header("UI Monedas")]
    public TextMeshProUGUI textoMonedas;

    private int monedas = 0;

    void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        ActualizarUI();
    }

    // Llamar desde cada escena para reconectar el texto de monedas
    public void RegistrarTextoMonedas(TextMeshProUGUI texto)
    {
        textoMonedas = texto;
        ActualizarUI();
    }

    public void AgregarMoneda()
    {
        monedas++;
        ActualizarUI();
    }

    public int ObtenerMonedas()
    {
        return monedas;
    }

    public bool GastarMonedas(int cantidad)
    {
        if (monedas >= cantidad)
        {
            monedas -= cantidad;
            ActualizarUI();
            return true;
        }
        return false;
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