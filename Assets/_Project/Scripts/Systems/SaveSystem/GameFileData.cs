using UnityEngine;

[System.Serializable]
public class GameFileData
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private float _size;
    [SerializeField] private CompressionLevel _compression;

    public GameFileData(GameFileData data)
    {
        _id = data._id;
        _name = data._name;
        _size = data._size;
        _compression = data._compression;
    }

    public string Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public float TotalSize { get => _size; set => _size = value; }
    public float Size => _size * Configs.CompressionSetting.GetModifier(_compression);
    public CompressionLevel CompressionLevel { get => _compression; set => _compression = value; }
}
