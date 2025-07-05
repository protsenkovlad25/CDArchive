using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsConfig", menuName = "Configs/GameSettingsConfig")]
public class GameSettingsConfig : ScriptableObject
{
    [SerializeField] private bool _isSaveData;

    public bool IsSaveData => _isSaveData;
}
