using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Retry()
    {
        int previousSceneIndex = PlayerPrefs.GetInt("PreviousSceneIndex", -1);

        if (previousSceneIndex != -1)
        {
            SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
