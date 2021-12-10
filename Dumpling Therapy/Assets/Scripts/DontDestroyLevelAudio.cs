using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyLevelAudio : MonoBehaviour
{
    private Scene currentScene;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name.Contains("Level"))
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
