using UnityEngine;
using UnityEngine.UI;

public class PersistentScoreText : MonoBehaviour
{
    Text highScoreText;
    DataSaver dataSaver;

    void Awake()
    {
        highScoreText = GetComponent<Text>();
    }

    void Start()
    {
        dataSaver = FindObjectOfType<DataSaver>();
        if (dataSaver == null) return;

        string name = dataSaver.GetName();

        if(name == "")
        {
            highScoreText.text = "High Score : 0";
        }
        else
        {
            name = dataSaver.GetName();
            int score = dataSaver.GetHighScore();

            highScoreText.text = "High Score : " + name + " : " + score;
        }
    }
}
