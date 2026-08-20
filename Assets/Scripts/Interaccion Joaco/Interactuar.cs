using UnityEngine;
using System.Collections;

public class Interactuar : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ActivarAnimacion()
    {
        gameObject.SetActive(true);

        animator.Play("Interactuar");

        audioSource.Play();

        StartCoroutine(DesactivarDespues());
    }

    IEnumerator DesactivarDespues()
    {
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }
}