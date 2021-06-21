using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataSaver : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    public void SaveData<T>(T toSave)
    {
        string json = JsonUtility.ToJson(toSave);
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json);
    }

    public string LoadData()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return json;
        }
        return "";
    }
}