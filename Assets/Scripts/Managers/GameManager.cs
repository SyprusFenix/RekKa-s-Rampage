using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int health;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public GameObject gameOverPanel;
    public Button restartButton;
    public Button quitButton;

    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(int healthChange)
    {
        health += healthChange;
        healthText.text = "Health: " + health;

        if (health == 0)
        {
            GameOver();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        health = 3;

        UpdateScore(0);
        UpdateHealth(0);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    // This is connected to the button in Unity, so the button can call this method
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Start Screen");
    }
}
