using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextScene()
    {
        Debug.Log("Iniciando transición");
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        // Ejecuta la animación
        Debug.Log("Activando animación");
        transition.SetTrigger("Start");

        // Espera a que termine
        yield return new WaitForSeconds(transitionTime);

        // Carga la siguiente escena
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex + 1
        );
    }
}