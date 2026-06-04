using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    [Header("Monedas")]
    public int monedas = 0;

    [Header("UI")]
    public TMP_Text textoMonedas; 

    void Awake()
    {
        
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ActualizarUI();
    }

    public void AgregarMoneda(int cantidad = 1)
    {
        monedas += cantidad;
        ActualizarUI();
        Debug.Log("Monedas: " + monedas);
    }

    void ActualizarUI()
    {
        if (textoMonedas != null)
            textoMonedas.text = "Monedas: " + monedas;
    }
}
