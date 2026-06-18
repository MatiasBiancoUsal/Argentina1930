using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    
    public string escenaDestino;

    private bool jugadorAdentro = false;

    void Update()
    {
        if (jugadorAdentro && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(escenaDestino);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorAdentro = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorAdentro = false;
    }
}