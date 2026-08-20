using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaJuego : MonoBehaviour
{
    [SerializeField] private GameObject panelPausa;

    private bool pausado = false;

    void Awake()
    {
        // Cada escena comienza siempre sin estar pausada
        Time.timeScale = 1f;
        pausado = false;

        if (panelPausa != null)
            panelPausa.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AlternarPausa();
        }
    }

    public void AlternarPausa()
    {
        pausado = !pausado;

        panelPausa.SetActive(pausado);

        if (pausado)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}