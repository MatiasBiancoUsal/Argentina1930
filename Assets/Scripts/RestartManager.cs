using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script independiente, solo para reiniciar el nivel cuando un enemigo atrapa al jugador.
// VERSIÓN CON LOGS DE DEBUG para diagnosticar por qué no reinicia.
// Una vez que funcione, podés borrar las líneas Debug.Log si no las querés.
public class RestartManager : MonoBehaviour
{
    public static RestartManager Instance;

    [Tooltip("Segundos de espera antes de reiniciar (para dar tiempo a una animación/sonido de 'atrapado').")]
    [SerializeField] private float delayReinicio = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Debug.Log("[RestartManager] Awake OK. Instance asignada.");
    }

    public void ReiniciarNivel()
    {
        Debug.Log("[RestartManager] ReiniciarNivel() llamado. delayReinicio = " + delayReinicio);
        StartCoroutine(ReiniciarNivelCoroutine());
    }

    private IEnumerator ReiniciarNivelCoroutine()
    {
        Debug.Log("[RestartManager] Coroutine iniciada. Esperando " + delayReinicio + "s (realtime, ignora pausa)...");
        yield return new WaitForSecondsRealtime(delayReinicio);

        Debug.Log("[RestartManager] Delay terminado. Time.timeScale antes de forzar = " + Time.timeScale);
        Time.timeScale = 1f;

        int index = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("[RestartManager] Llamando a LoadScene con buildIndex = " + index);

        SceneManager.LoadScene(index);

        Debug.Log("[RestartManager] LoadScene ejecutado (esta línea se ve solo si LoadScene no interrumpe el frame).");
    }
}