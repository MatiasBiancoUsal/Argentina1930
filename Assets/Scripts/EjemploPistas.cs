using UnityEngine;

// este script es de ejemplo para ver como funcionan el sistema de pistas para aplicar a otros scripts.

public class PruebaPista : MonoBehaviour
{
    public LibretaPistas libreta;

    private bool pistaAgregada = false;
    public string pista;

    void Start()
    {
       // libreta.AgregarPista("Un testigo vio al alcalde salir del banco a las 23:00.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !pistaAgregada)
        {
            libreta.AgregarPista(pista);
            pistaAgregada = true;
        }
    }
}
