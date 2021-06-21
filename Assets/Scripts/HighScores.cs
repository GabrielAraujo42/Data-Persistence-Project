using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    DataSaver dataSaver;
    InputField nameField;
    [SerializeField] List<HighScoreData> scores = new List<HighScoreData>();

    string currentPlayerName = null;
    int listMaxLength = 5;

    void Awake()
    {
        if (FindObjectsOfType<HighScores>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        InitializeHighScore();

        nameField = FindObjectOfType<InputField>();
        dataSaver = GetComponent<DataSaver>();

        string[] json = dataSaver.LoadScoreData();
        if(json != null)
        {
            scores = new List<HighScoreData>();
            foreach (string score in json)
            {
                scores.Add(JsonUtility.FromJson<HighScoreData>(score));
            }
        }
    }

    public void UpdateHighScoreList(int score)
    {
        HighScoreData newScore = new HighScoreData(currentPlayerName, score);
        AddHighScore(newScore);

        dataSaver.SaveScoreData(scores.ToArray());
    }

    public bool StartGame()
    {
        if (nameField == null) return false;
        if (nameField.text == null) return false;

        SetCurrentPlayer();
        return true;
    }

    void AddHighScore(HighScoreData newScore)
    {
        scores.Add(newScore);
        scores.Sort(CompareScores);
        if(scores.Count > listMaxLength)
        {
            scores.RemoveAt(listMaxLength);
        }
    }

    int CompareScores(HighScoreData scoreX, HighScoreData scoreY)
    {
        return scoreY.score.CompareTo(scoreX.score);
    }

    void InitializeHighScore()
    {
        scores = new List<HighScoreData>();
        scores.Add(new HighScoreData());
    }

    void SetCurrentPlayer()
    {
        currentPlayerName = nameField.text;
    }

    public string GetName(int index)
    {
        if(index >= scores.Count)
        {
            return "";
        }
        return scores[index].name;
    }

    public int GetHighScore(int index)
    {
        return scores[index].score;
    }
}