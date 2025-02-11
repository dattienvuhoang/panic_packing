using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public  class JsonManager : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
    }

    public void SaveData(UserData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        PlayerPrefs.SetString("PlayerData", json);
        PlayerPrefs.Save();
        Debug.Log("Data saved to: " + filePath);
    }

    public UserData LoadData()
    {
        if (PlayerPrefs.HasKey("UserData"))
        {
            string json = PlayerPrefs.GetString("PlayerData");
            UserData data = JsonUtility.FromJson<UserData>(json);
            Debug.Log("Data loaded from PlayerPrefs: " + json);
            return data;
        }
        else if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            UserData data = JsonUtility.FromJson<UserData>(json);
            Debug.Log("Data loaded from file: " + json);
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found!");
            return null;
        }
    }
}
