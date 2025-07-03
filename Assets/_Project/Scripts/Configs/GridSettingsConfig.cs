using UnityEngine;

[CreateAssetMenu(fileName = "GridSettingsConfig", menuName = "Configs/GridSettingsConfig")]
public class GridSettingsConfig : ScriptableObject
{
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Vector2 _offset;

    public Cell CellPrefab => _cellPrefab;
    public Vector2 Offset => _offset;
    public float Spacing => _cellPrefab.Size;
    public float CellSize => _cellPrefab.Size;

    public Vector2 GetAlignPos(Vector2 position)
    {
        float spacing = Spacing;
        float alignedX = Mathf.Round((position.x - _offset.x) / spacing) * spacing + _offset.x;
        float alignedY = Mathf.Round((position.y - _offset.y) / spacing) * spacing + _offset.y;

        return new Vector2(alignedX, alignedY);
    }
}
