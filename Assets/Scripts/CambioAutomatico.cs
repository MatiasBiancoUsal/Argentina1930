using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CambioAutomatico : MonoBehaviour
{
    public float tiempoEspera = 5.3f;

    // Nombre de la escena a la que va a ir este cambio automático.
    // Se escribe a mano en el Inspector (debe coincidir EXACTO
    // con el nombre del archivo de la escena, sin ".unity").
    public string sceneToLoad;

    void Start()
    {
        StartCoroutine(CambiarEscena());
    }

    IEnumerator CambiarEscena()
    {
        yield return new WaitForSeconds(tiempoEspera);

        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogWarning("No se asignó ninguna escena en sceneToLoad.");
            yield break;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}