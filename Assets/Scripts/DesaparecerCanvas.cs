using UnityEngine;

public class DesaparecerCanvas : MonoBehaviour
{
    private bool desaparecido = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !desaparecido)
        {
            gameObject.SetActive(false);
            desaparecido = true;
        }
    }
}
