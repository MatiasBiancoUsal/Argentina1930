using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void IrAJuego()
    {
        // ANALYTICS: level_start (arranca el nivel y los cronómetros)
        AnalyticsManager.Instance?.LevelStart(1);

        SceneManager.LoadScene(1);
    }

    public void AtajosTeclado()
    {
        SceneManager.LoadScene(8);
    }

    public void IrACreditos()
    {
        SceneManager.LoadScene(9);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
