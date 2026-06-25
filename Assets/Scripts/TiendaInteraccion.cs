using UnityEngine;

public class TiendaInteraccion : MonoBehaviour
{
    [SerializeField] private GameObject menuTienda;
    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            bool abierto = menuTienda.activeSelf;
            menuTienda.SetActive(!abierto);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            menuTienda.SetActive(false);
        }
    }
}