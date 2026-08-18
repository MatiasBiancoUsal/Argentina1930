using UnityEngine;

public class PausaJuego : MonoBehaviour
{
    [SerializeField] private GameObject panelPausa;

    private bool pausado = false;

    void Start()
    {
        panelPausa.SetActive(false);
        Time.timeScale = 1f;
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
    UnityEngine.SceneManagement.SceneManager.LoadScene("MenuPrincipal");
}
}