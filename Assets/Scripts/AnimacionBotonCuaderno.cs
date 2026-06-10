using UnityEngine;
using UnityEngine.UI;

public class BotonLibreta : MonoBehaviour
{
    public Animator animator;
    public Image imagenBoton;
    public Sprite spriteCerrado;

    public void AbrirLibreta()
    {
        animator.Play("AbrirCuaderno", 0, 0f);
    }

    public void CerrarLibreta()
    {
           animator.Rebind();
           animator.Update(0f);
    }
}