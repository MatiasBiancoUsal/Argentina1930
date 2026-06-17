using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void IrAJuego()
    {
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
