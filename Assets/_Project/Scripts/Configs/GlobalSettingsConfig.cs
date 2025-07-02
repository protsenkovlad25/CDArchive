using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalSettingsConfig", menuName = "Configs/GlobalSettingsConfig")]
public class GlobalSettingsConfig : ScriptableObject
{
    [SerializeField] private DiscConfig _discSetting;
    [SerializeField] private List<GameFileConfig> _gameFileSettings;
    [SerializeField] private List<LevelSettingsConfig> _levelSettings;

    public DiscConfig DiscSetting => _discSetting;
    public List<GameFileConfig> GameFileSettings => _gameFileSettings;
    public List<LevelSettingsConfig> LevelSettings => _levelSettings;
}
