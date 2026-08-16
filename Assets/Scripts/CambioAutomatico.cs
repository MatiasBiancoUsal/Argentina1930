using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CambioAutomatico : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CambiarEscena());
    }

    IEnumerator CambiarEscena()
    {
        yield return new WaitForSeconds(5.3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
