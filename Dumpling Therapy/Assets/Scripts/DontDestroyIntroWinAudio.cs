using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyIntroWinAudio : MonoBehaviour
{
    private Scene currentScene;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name.Contains("Intro") || currentScene.name.Contains("Win"))
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}