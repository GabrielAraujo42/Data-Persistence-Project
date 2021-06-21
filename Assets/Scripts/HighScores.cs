using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    DataSaver dataSaver;
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

        dataSaver = GetComponent<DataSaver>();

        LoadData();
    }

    public void LoadData()
    {
        string[] json = dataSaver.LoadScoreData();
        scores = new List<HighScoreData>();
        if (json != null)
        {
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
        InputField inputField = FindObjectOfType<InputField>();
        if (inputField == null) return false;
        if (inputField.text == null) return false;

        SetCurrentPlayer(inputField);
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

    void SetCurrentPlayer(InputField inputField)
    {
        currentPlayerName = inputField.text;
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