using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataSaver : MonoBehaviour
{
    InputField nameField;
    HighScoreData highScore;
    string currentPlayerName = null;

    void Awake()
    {
        InitializeHighScore();
        if (FindObjectsOfType<DataSaver>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        nameField = FindObjectOfType<InputField>();

        LoadData();
    }

    public string GetName()
    {
        return highScore.name;
    }

    public int GetHighScore()
    {
        return highScore.score;
    }

    public void UpdateHighScore(int score)
    {
        highScore.name = currentPlayerName;
        highScore.score = score;

        SaveData();
    }

    public void StartGame()
    {
        if (nameField == null) return;
        if (nameField.text == null) return;

        SetCurrentPlayer();
        LoadNextScene();
    }

    void InitializeHighScore()
    {
        highScore = new HighScoreData();
        highScore.name = "";
        highScore.score = 0;
    }

    void SetCurrentPlayer()
    {
        currentPlayerName = nameField.text;
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    void SaveData()
    {
        string json = JsonUtility.ToJson(highScore);
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json);
    }

    void LoadData()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            highScore = JsonUtility.FromJson<HighScoreData>(json);
        }
    }

    [System.Serializable]
    class HighScoreData
    {
        public string name;
        public int score;
    }
}