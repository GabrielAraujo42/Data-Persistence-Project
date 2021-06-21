using UnityEngine;
using UnityEngine.UI;

public class PersistentScoreText : MonoBehaviour
{
    Text highScoreText;
    HighScores highScores;

    public int highScoreToShow = 0;

    void Awake()
    {
        highScoreText = GetComponent<Text>();
    }

    void Start()
    {
        highScores = FindObjectOfType<HighScores>();
        if (highScores == null) return;

        string name = highScores.GetName(highScoreToShow);

        if(name == "")
        {
            highScoreText.text = "High Score : 0";
        }
        else
        {
            name = highScores.GetName(highScoreToShow);
            int score = highScores.GetHighScore(highScoreToShow);

            highScoreText.text = "High Score : " + name + " : " + score;
        }
    }
}
