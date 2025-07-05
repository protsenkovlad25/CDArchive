using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettingsConfig", menuName = "Configs/LevelSettingsConfig")]
public class LevelSettingsConfig : ScriptableObject
{
    [SerializeField] private GridData _gridData;
    [SerializeField] private Vector2Int _playerStartPos;
    [SerializeField] private Vector2Int _enemyStartPos;
    [SerializeField] private int _playerHealths;
    [SerializeField] private float _completePercent;
    [SerializeField] private float _shootInterval;

    public GridData GridData => _gridData;
    public Vector2Int PlayerStartPos => _playerStartPos;
    public Vector2Int EnemyStartPos => _enemyStartPos;
    public int PlayerHealths => _playerHealths;
    public float CompletePercent => _completePercent;
    public float ShootInterval => _shootInterval;
}
