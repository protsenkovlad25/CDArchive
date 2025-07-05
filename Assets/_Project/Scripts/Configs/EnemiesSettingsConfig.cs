using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesSettingsConfig", menuName = "Configs/EnemiesSettingsConfig")]
public class EnemiesSettingsConfig : ScriptableObject
{
    [SerializeField] private SerializedDictionary<EnemyType, Unit> _prefabByType;

    public Unit GetPrefab(EnemyType type)
    {
        return _prefabByType[type];
    }
}
