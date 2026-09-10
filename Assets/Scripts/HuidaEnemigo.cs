using UnityEngine;

public class HuidaEnemigo : MonoBehaviour
{
    public GameObject ObjetoAmover1;
   

    public Transform StartPoint;
    public Transform EndPoint;

    public float Velocidad = 1f;

    public Sprite spriteVigilante;

    private SpriteRenderer spriteRenderer;
    private bool mover = false;

    private void Start()
    {
        // Coloca el objeto en el punto inicial
        ObjetoAmover1.transform.position = StartPoint.position;

        // Obtiene el SpriteRenderer
        spriteRenderer = ObjetoAmover1.GetComponent<SpriteRenderer>();
        
        
    }

    private void Update()
    {
        if (mover)
        {
            
            ObjetoAmover1.transform.position = Vector3.MoveTowards(
                ObjetoAmover1.transform.position,
                EndPoint.position,
                Velocidad * Time.deltaTime
            );

            // Cuando llega al destino, deja de moverse
            if (ObjetoAmover1.transform.position == EndPoint.position)
            {
                mover = false;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (mover == false)
            {
                Invoke("Delay" , 5f);
            }
            
        }
    }
    void Delay ()
    {
        mover = true ;
    }
}