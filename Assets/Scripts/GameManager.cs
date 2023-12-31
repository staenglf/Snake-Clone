using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Snake sn;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public GameObject button;
    private int score;
    private int highscore;
    private AudioSource source;

    // Start is called before the first frame update
    private void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        highscoreText.text = highscore.ToString();
        source = GetComponent<AudioSource>();
    }

    // Adds 1 to the score by every food collission an overwrites the highscore
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        if(score > highscore)
        {
            highscore = score;
            highscoreText.text = highscore.ToString();
        }
    }

    // Saves the highscore in the PlayerPrefs when the game gets closed
    private void SaveHighscore()
    {
        if(highscore > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", highscore);
        }
    }

    // Deletes the attached rectangles and opens the restart button
    public void RestartGame()
    {
        source.Play();

        button.SetActive(true);

        for (int i = 1; i < sn.segments.Count; i++)
        {
            Destroy(sn.segments[i]);
        }

        sn.segments.RemoveRange(1, sn.segments.Count - 1);
    }

    //Closes the restart button, sets the snake to the start point and sets the highscore to zero
    public void ResetTheGame()
    {
        button.SetActive(false);

        sn.transform.position = Vector2.zero;

        score = 0;
        scoreText.text = score.ToString();

        SaveHighscore();
    }
}
