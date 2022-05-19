using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int score;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text gameOverScoreText;
    [SerializeField]
    private Text highScoreText;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
    }
    void Start()
    {
        AddScore(0);
    }

    void Update()
    {
    }
    public void AddScore(int value)
    {
        score += value;
        if (score>PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        scoreText.text = score.ToString();
    }
    public void ResetScore()
    {
        score = 0;
    }
    public void GameOver()
    {
        gameOverScoreText.text = score.ToString();
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
