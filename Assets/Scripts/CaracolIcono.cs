using UnityEngine;

public class CaracolIcono : MonoBehaviour
{
    public GameObject icono;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            icono.SetActive(!icono.activeSelf);
        }
    }
}
