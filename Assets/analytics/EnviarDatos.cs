using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnviarDatos : MonoBehaviour
{

    public int vidas;
    public int municion;

    public void EnviarDato()
    {
        Scene escenaActual = SceneManager.GetActiveScene();
        CustomEvent iniciarnivel = new CustomEvent("level_start")
        {
            {"NombreNivel", escenaActual.buildIndex},
            {"Vidas del jugador", vidas},
            {"municion del jugador", municion}
        };
    }


    }
