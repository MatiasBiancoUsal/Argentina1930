```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public string escenaDestino;

    [Header("Canvas")]
    [SerializeField] private GameObject canvasHabitacion;

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
        {
            jugadorAdentro = true;

            // Activar Canvas
            canvasHabitacion.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorAdentro = false;

            // Desactivar Canvas
            canvasHabitacion.SetActive(false);
        }
    }
}
```