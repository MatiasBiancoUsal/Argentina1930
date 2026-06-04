using UnityEngine;

public class Banco : MonoBehaviour

{
    public bool Cartel = true;
    [SerializeField] private GameObject cartelPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cartelPrefab.SetActive(true);
            Debug.Log(("ACTIVADO"));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cartelPrefab.SetActive(false);
            Debug.Log(("DESACTIVADO"));
        }
    }

}
