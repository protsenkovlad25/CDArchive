using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiscData
{
    [JsonProperty("capacity")]
    [SerializeField] private float _capacity;
    [JsonProperty("usedSpace")]
    [SerializeField] private float _usedSpace;
    [JsonProperty("files")]
    [SerializeField] private List<GameFileData> _files;

    [JsonConstructor]
    public DiscData(float capacity, float usedSpace, List<GameFileData> files)
    {
        _capacity = capacity;
        _usedSpace = usedSpace;
        _files = files;
    }
    public DiscData(DiscData data)
    {
        _capacity = data.Capacity;
        _usedSpace = data.UsedSpace;
        _files = data.Files;
    }

    [JsonIgnore] public float Capacity { get => _capacity; set => _capacity = value; }
    [JsonIgnore] public float UsedSpace { get => _usedSpace; set => _usedSpace = value; }
    [JsonIgnore] public List<GameFileData> Files { get => _files; set => _files = value; }

    public void AddFile(GameFileData file)
    {
        _files.Add(file);

        _usedSpace += file.Size;
    }
}
