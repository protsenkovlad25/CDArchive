using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettingsConfig", menuName = "Configs/LevelSettingsConfig")]
public class LevelSettingsConfig : ScriptableObject
{
    [SerializeField] private GridData _gridData;
    [SerializeField] private Vector2Int _playerStartPos;
    [SerializeField] private int _enemiesCount;
    [SerializeField] private float _completePercent;
    [SerializeField] private float _shootInterval;

    public GridData GridData => _gridData;
    public Vector2Int PlayerStartPos => _playerStartPos;
    public int EnemiesCount => _enemiesCount;
    public float CompletePercent => _completePercent;
    public float ShootInterval => _shootInterval;
}
