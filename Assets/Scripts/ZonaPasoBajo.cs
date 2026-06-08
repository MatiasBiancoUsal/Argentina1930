using UnityEngine;

public class ZonaPasoBajo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Debug.Log("Agáchate con C para pasar");
    }
}