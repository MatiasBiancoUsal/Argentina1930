using UnityEngine;

public class Acusación : MonoBehaviour
{
    public GameObject ObjetoAmover;

    public Transform StartPoint;
    public Transform EndPoint;
    

    public float Velocidad;

    private Vector3 MoverHacia;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        MoverHacia = EndPoint.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MoverHacia = EndPoint.position;
            
        }
    }

}
