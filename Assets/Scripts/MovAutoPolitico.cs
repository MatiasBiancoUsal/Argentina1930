using UnityEngine;

public class MovAutoPolitico : MonoBehaviour
{
    public GameObject ObjetoAmover;

    public Transform StartPoint;
    public Transform EndPoint;

    public float Velocidad;

    private Vector3 MoverHacia;
    


    private void Start()
    {
        MoverHacia = EndPoint.position;
        
    }
    private void Update()
    {
        ObjetoAmover.transform.position = Vector3.MoveTowards(ObjetoAmover.transform.position, MoverHacia, Velocidad * Time.deltaTime);
        
        if (ObjetoAmover.transform.position == StartPoint.position)
        {
            MoverHacia = EndPoint.position;
            
        }
    }

}
