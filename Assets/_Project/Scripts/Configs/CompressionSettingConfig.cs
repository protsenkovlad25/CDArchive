using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "CompressionSettingConfig", menuName = "Configs/CompressionSettingConfig")]
public class CompressionSettingConfig : ScriptableObject
{
    [System.Serializable]
    private class CompressionData
    {
        [SerializeField] private float _modifier;

        public float Modifier => _modifier;
    }

    [SerializeField] private int _levelsCount;
    [SerializeField] private SerializedDictionary<CompressionLevel, CompressionData> _compressionDatas;

    public int LevelsCount => _levelsCount;

    public float GetModifier(CompressionLevel lvl)
    {
        return _compressionDatas[lvl].Modifier;
    }
}
