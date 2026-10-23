using Unity.Cinemachine;
using UnityEngine;

public class MovEnemigosHorizontal : MonoBehaviour

{
    public Animator animator;
    public float velocidad = 3f;
    private bool moviendose = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool ("huye" , false);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            moviendose=true;
        }
    }
    void Update()
    {
        if (moviendose)
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);
        animator.SetBool("huye", true);

    }
}