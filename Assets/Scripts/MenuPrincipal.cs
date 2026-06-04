using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void IrAJuego()
    {
        SceneManager.LoadScene(1);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
