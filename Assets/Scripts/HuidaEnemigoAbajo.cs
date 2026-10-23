using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
public class HuidaEnemigoAbajo : MonoBehaviour

{

    public float velocidad = 3f;
    public Animator animator;
    public float delay = 5f;

    private bool moviendose = false;
    private bool activado = false;
    void Update()
    {
     if (moviendose)
        {
            transform.Translate(Vector2.down * velocidad * Time.deltaTime, Space.World);
            animator.SetBool("huye", true);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") & !activado)
        {
            activado=true;
            StartCoroutine(EsperarYMover());
        }
    }
    IEnumerator EsperarYMover()
    {
        yield return new WaitForSeconds(delay);
        moviendose=true;
    }
}
