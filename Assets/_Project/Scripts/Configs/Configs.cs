using UnityEngine;

public static class Configs
{
    private static readonly string _mainPath = "Configs";
    private static readonly string _itemsPath = "Configs/Items";

    public static GlobalSettingsConfig GlobalSettings => Resources.Load<GlobalSettingsConfig>($"{_mainPath}/Global/GlobalSettingsConfig");
    public static CompressionSettingConfig CompressionSetting => Resources.Load<CompressionSettingConfig>($"{_itemsPath}/Compression/CompressionSettingConfig");
}
