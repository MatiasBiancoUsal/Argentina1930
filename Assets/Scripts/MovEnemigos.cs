using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovEnemigos : MonoBehaviour
{
    public GameObject ObjetoAmover;

    public Transform StartPoint;
    public Transform EndPoint;

    public float Velocidad;

    private Vector3 MoverHacia;

    private Animator animator;

    public Sprite spriteVigilante;

    private SpriteRenderer spriteRenderer;

    [Header("Analytics")]
    [SerializeField] private string tipoEnemigo = "Vigilante";
    [SerializeField] private int danioAlAtrapar = 1;

    private void Start()
    {
        MoverHacia = EndPoint.position;
        animator = ObjetoAmover.GetComponent<Animator>();
        spriteRenderer = ObjetoAmover.GetComponent<SpriteRenderer>();
        animator.SetBool("espalda", true);
    }

    private void Update()
    {
        ObjetoAmover.transform.position = Vector3.MoveTowards(ObjetoAmover.transform.position, MoverHacia, Velocidad * Time.deltaTime);
        if (ObjetoAmover.transform.position == EndPoint.position)
        {
            MoverHacia = StartPoint.position;
            animator.SetBool("espalda", false);
        }
        if (ObjetoAmover.transform.position == StartPoint.position)
        {
            MoverHacia = EndPoint.position;
            animator.SetBool("espalda", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // ANALYTICS: enemy_detect_player + player_hit
            AnalyticsManager.Instance?.EnemyDetectPlayer(tipoEnemigo);
            AnalyticsManager.Instance?.PlayerHit(danioAlAtrapar);

            animator.enabled = false;

            spriteRenderer.sprite = spriteVigilante;

            enabled = false;

            StartCoroutine(ReiniciarEscena());
        }
    }

    private IEnumerator ReiniciarEscena()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
