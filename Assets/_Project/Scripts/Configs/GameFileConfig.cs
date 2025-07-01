using UnityEngine;

[CreateAssetMenu(fileName = "GameFileConfig", menuName = "Configs/GameFileConfig")]
public class GameFileConfig : ScriptableObject
{
    [SerializeField] private GameFileData _gameFileData;

    public GameFileData GameFileData => _gameFileData;
}
