using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    DataSaver dataSaver;
    InputField nameField;
    List<HighScoreData> scores = new List<HighScoreData>();

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

        string json = dataSaver.LoadData();
        if(json != "")
        {
            scores = JsonUtility.FromJson<List<HighScoreData>>(json);
        }
    }

    public void UpdateHighScoreList(int score)
    {
        HighScoreData newScore = new HighScoreData(currentPlayerName, score);
        AddHighScore(newScore);

        dataSaver.SaveData<List<HighScoreData>>(scores);
    }

    public void StartGame()
    {
        if (nameField == null) return;
        if (nameField.text == null) return;

        SetCurrentPlayer();
        dataSaver.LoadNextScene();
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
        return scoreX.score.CompareTo(scoreY.score);
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
        return scores[index].name;
    }

    public int GetHighScore(int index)
    {
        return scores[index].score;
    }
}