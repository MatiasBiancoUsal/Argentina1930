
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CambioAutomatico : MonoBehaviour
{
    public float tiempoEspera = 5.3f;

    // Nombre de la escena a la que se va a cambiar.
    public string sceneToLoad;

    private bool escenaCambiada = false;

    void Start()
    {
        StartCoroutine(CambiarEscena());
    }

    void Update()
    {
        // Cambiar de escena al presionar la tecla E.
        if (Input.GetKeyDown(KeyCode.E))
        {
            CargarEscena();
        }
    }

    IEnumerator CambiarEscena()
    {
        yield return new WaitForSeconds(tiempoEspera);

        CargarEscena();
    }

    void CargarEscena()
    {
        // Evitar que la escena se cargue dos veces.
        if (escenaCambiada)
            return;

        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogWarning("No se asignó ninguna escena en sceneToLoad.");
            return;
        }

        escenaCambiada = true;
        SceneManager.LoadScene(sceneToLoad);
    }
}
