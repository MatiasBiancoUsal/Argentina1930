using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditosScroll : MonoBehaviour
{
    public RectTransform texto;
    public float velocidad = 50f;

    public float tiempoParaSalir = 10f; // segundos hasta volver al menú
    private float tiempo = 0f;

    void Update()
    {
        // Subir texto
        texto.anchoredPosition += Vector2.up * velocidad * Time.deltaTime;

        // Contador de tiempo
        tiempo += Time.deltaTime;

        // Volver automáticamente a la escena 0
        if (tiempo >= tiempoParaSalir)
        {
            SceneManager.LoadScene(0);
        }

        // Salir manual con Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
        }
    }
}
