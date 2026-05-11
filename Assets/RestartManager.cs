using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Important: Resume time before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

