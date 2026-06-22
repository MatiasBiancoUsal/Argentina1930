using UnityEngine;
using TMPro;

public class ConectarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoMonedas;

    void Start()
    {
        GameManager.Instancia?.RegistrarTextoMonedas(textoMonedas);
    }
}