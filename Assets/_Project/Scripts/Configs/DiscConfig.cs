using UnityEngine;

[CreateAssetMenu(fileName = "DiscConfig", menuName = "Configs/DiscConfig")]
public class DiscConfig : ScriptableObject
{
    [SerializeField] private DiscData _discData;

    public DiscData DiscData => _discData;

    private void OnValidate()
    {
        float usedSpace = 0;

        foreach (var file in _discData.Files)
            usedSpace += file.Size;

        _discData.UsedSpace = usedSpace;
    }
}
