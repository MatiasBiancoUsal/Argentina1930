using UnityEngine;
using UnityEngine.SceneManagement;

public class IrADormir : MonoBehaviour
{
    private bool playerNear = false;

    private void Update()
    {
        if (playerNear &&
            LevelManager.Instance.IrADormir &&
            Input.GetKeyDown(KeyCode.J))
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}