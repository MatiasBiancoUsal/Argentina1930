using UnityEngine;

public class Cama : MonoBehaviour
{
    [SerializeField] private GameObject cartelPrefab;
    [SerializeField] private SceneTransition sceneTransition;

    private bool jugadorCerca = false;

    private void Update()
    {
        // Si el jugador está cerca y presiona E
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Durmiendo...");
             Debug.Log("E presionada");

            sceneTransition.LoadNextScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;

            cartelPrefab.SetActive(true);
            Debug.Log("ACTIVADO");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;

            cartelPrefab.SetActive(false);
            Debug.Log("DESACTIVADO");
        }
    }
}