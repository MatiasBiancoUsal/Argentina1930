using UnityEngine;
using UnityEngine.SceneManagement;

public class iraescena0 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("l"))
        {
            SceneManager.LoadScene(0);
        }



    }
}
