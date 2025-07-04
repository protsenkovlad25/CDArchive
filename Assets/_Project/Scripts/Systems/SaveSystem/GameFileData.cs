using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class GameFileData
{
    [JsonProperty("id")]
    [SerializeField] private string _id;
    [JsonProperty("name")]
    [SerializeField] private string _name;
    [JsonProperty("size")]
    [SerializeField] private float _size;
    [JsonProperty("compression")]
    [SerializeField] private CompressionLevel _compression;

    [JsonConstructor]
    public GameFileData(string id, string name, float size, CompressionLevel compression)
    {
        _id = id;
        _name = name;
        _size = size;
        _compression = compression;
    }
    public GameFileData(GameFileData data)
    {
        _id = data.Id;
        _name = data.Name;
        _size = data.TotalSize;
        _compression = data.CompressionLevel;
    }

    [JsonIgnore] public string Id { get => _id; set => _id = value; }
    [JsonIgnore] public string Name { get => _name; set => _name = value; }
    [JsonIgnore] public float TotalSize { get => _size; set => _size = value; }
    [JsonIgnore] public float Size => _size * Configs.CompressionSetting.GetModifier(_compression);
    [JsonIgnore] public CompressionLevel CompressionLevel { get => _compression; set => _compression = value; }

    public void UpdateCompressionLevel(int compressionLvl)
    {
        if (compressionLvl > (int)_compression)
            _compression = (CompressionLevel)compressionLvl;
    }
}
