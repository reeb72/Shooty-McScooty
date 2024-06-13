using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public void GameOver()
    {
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("PreviousSceneIndex", previousSceneIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

}
