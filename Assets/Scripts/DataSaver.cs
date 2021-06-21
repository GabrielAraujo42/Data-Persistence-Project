using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public void SaveScoreData(HighScoreData[] toSave)
    {
        string json = "";
        foreach (HighScoreData data in toSave)
        {
            json += JsonUtility.ToJson(data) + ";";
        }
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json);
    }

    public string[] LoadScoreData()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return json.Split(';');
        }
        return null;
    }

    public void ResetScores()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}