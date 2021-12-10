using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int lives = 3;
    private int currentScore;
    public Text livesText;
    public Text scoreText;
    public bool gameOver;
    AudioSource bonusLifeSound;
    void Start()
    {
        livesText.text = "" + lives;
        scoreText.text = "" + currentScore;
        bonusLifeSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameOver)
        {
            SceneManager.LoadScene("GameOver");
        }
        CheckBonusForPoints();
        CheckIfLevelCompleted();
    }
    public void UpdateLives(int updateLives)
    {
        lives += updateLives;

        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }
        livesText.text = "" + lives;
    }

    public void UpdateScore(int points)
    {
        currentScore += points;

        scoreText.text = "" + currentScore;
    }

    void GameOver()
    {
        gameOver = true;
    }

    void CheckIfLevelCompleted()
    {
        if (GameObject.FindWithTag("Brick") == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void CheckBonusForPoints()
    {
        if(currentScore >= 5000)
        {
            currentScore -= 5000;
            UpdateLives(1);
            bonusLifeSound.Play();
        }
    }
}
