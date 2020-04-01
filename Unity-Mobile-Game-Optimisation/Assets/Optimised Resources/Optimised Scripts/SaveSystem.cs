using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void saveSetting(SettingCanvas settingData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(settingData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadSettingData()
    {
        string path = Application.persistentDataPath + "/GameData.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save setting not found in " + path);
            return null;
        }
    }
}