using UnityEngine;

public class AnimacionBoton : MonoBehaviour
{
    public Animator animator;

    public void ReproducirAnimacion()
    {
        animator.SetTrigger("Presionar");
    }
}