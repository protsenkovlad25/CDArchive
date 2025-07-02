using UnityEngine;

public class GridBorders : MonoBehaviour
{
    [SerializeField] private GridBorder _top;
    [SerializeField] private GridBorder _bottom;
    [SerializeField] private GridBorder _left;
    [SerializeField] private GridBorder _right;

    public void UpdateBorders(Vector2Int size, float spacing, Vector2 offset)
    {
        float gridWidth = (size.x + 1) * spacing;
        float gridHeight = (size.y + 1) * spacing;

        Vector3 extraOffset = new(offset.x, offset.y, 0f);

        _top.transform.position = new(extraOffset.x, gridHeight / 2f + extraOffset.y, 0f);
        _top.SetColliderSize(new(gridWidth + spacing, spacing));

        _bottom.transform.position = new(extraOffset.x, -(gridHeight / 2f) + extraOffset.y, 0f);
        _bottom.SetColliderSize(new(gridWidth + spacing, spacing));

        _left.transform.position = new(-(gridWidth / 2f) + extraOffset.x, extraOffset.y, 0f);
        _left.SetColliderSize(new(spacing, gridHeight + spacing));

        _right.transform.position = new(gridWidth / 2f + extraOffset.x, extraOffset.y, 0f);
        _right.SetColliderSize(new(spacing, gridHeight + spacing));
    }
}
