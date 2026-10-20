using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour

{
    private Animator animator;
    private Vector2 movimiento;
    public float Velocidad = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetFloat("MovimientoX", movimiento.x);
        animator.SetFloat("MovimientoY", movimiento.y);

        animator.SetBool("Caminando", movimiento.x != 0 || movimiento.y != 0);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Caminando", movimiento.x != 0 || movimiento.y != 0);
        animator.SetBool("Caminando", false);

        // Animacion Idle
        if (movimiento.x != 0 || movimiento.y != 0)
        {
            animator.SetBool("Caminando", true);
            animator.SetFloat("UltimoX", movimiento.x);
            animator.SetFloat("UltimoY", movimiento.y);

        }
    }
}
