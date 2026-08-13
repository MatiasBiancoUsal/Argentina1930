using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    private static SceneTransition instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextScene()
    {
        Debug.Log("Iniciando transición");
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        // Hace la transición hacia negro
        Debug.Log("Activando animación");
        transition.SetTrigger("Start");

        // Espera a que llegue completamente a negro
        yield return new WaitForSeconds(transitionTime);

        // Cambia de escena mientras la pantalla sigue negra
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex + 1
        );
    }
}