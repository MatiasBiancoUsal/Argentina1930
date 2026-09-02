using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    // Nombre de la escena a la que va a ir esta transición.
    // Se escribe a mano en el Inspector (debe coincidir EXACTO
    // con el nombre del archivo de la escena, sin ".unity").
    public string sceneToLoad;

    // Usa el nombre cargado en el Inspector (sceneToLoad)
    public void LoadNextScene()
    {
        LoadScene(sceneToLoad);
    }

    // Permite pasar el nombre de la escena por parámetro,
    // por ejemplo desde otro script o desde un botón con argumento.
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("No se asignó ninguna escena en sceneToLoad.");
            return;
        }

        Debug.Log("Iniciando transición hacia: " + sceneName);
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        // Hace la transición hacia negro
        Debug.Log("Activando animación");
        transition.SetTrigger("Start");

        // Espera a que llegue completamente a negro
        yield return new WaitForSeconds(transitionTime);

        // Cambia de escena mientras la pantalla sigue negra
        SceneManager.LoadScene(sceneName);
    }
}