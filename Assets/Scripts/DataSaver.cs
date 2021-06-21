using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
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