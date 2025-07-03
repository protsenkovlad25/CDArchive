using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    [JsonProperty("disc")]
    [SerializeField] private DiscData _discData;
    [JsonProperty("gameFiles")]
    [SerializeField] private List<GameFileData> _gameFiles;

    public PlayerData()
    {
        _discData = new(Configs.GlobalSettings.DiscSetting.DiscData);
        _gameFiles = new();
    }
    [JsonConstructor]
    public PlayerData(DiscData discData, List<GameFileData> gameFiles)
    {
        _discData = discData;
        _gameFiles = gameFiles;
    }

    [JsonIgnore] public DiscData DiscData { get => _discData; set => _discData = value; }
    [JsonIgnore] public List<GameFileData> GameFiles { get => _gameFiles; set => _gameFiles = value; }
}
