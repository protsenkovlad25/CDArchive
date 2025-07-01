using UnityEngine;

[CreateAssetMenu(fileName = "DiscConfig", menuName = "Configs/DiscConfig")]
public class DiscConfig : ScriptableObject
{
    [SerializeField] private DiscData _discData;

    public DiscData DiscData => _discData;
}
