using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class SaveController
{
    private readonly string PLAYER_FILE_PATH = $"{Application.persistentDataPath}/PlayerData.json";

    public SaveController()
    {
    }

    public void SavePlayerData(PlayerData playerData)
    {
        string jsonFile = JsonConvert.SerializeObject(playerData);
        File.WriteAllText(PLAYER_FILE_PATH, jsonFile);
    }

    public PlayerData LoadPlayerData()
    {
        PlayerData data;
        if (File.Exists(PLAYER_FILE_PATH))
        {
            string jsonFile = File.ReadAllText(PLAYER_FILE_PATH);
            data = JsonConvert.DeserializeObject<PlayerData>(jsonFile);
        }
        else data = new PlayerData();

        return data;
    }
}
