using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Retry()
    {
        int previousSceneIndex = PlayerPrefs.GetInt("PreviousSceneIndex", 0);
        
        if (previousSceneIndex != 0)
        {
            SceneManager.LoadScene(previousSceneIndex);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
