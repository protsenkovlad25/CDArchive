using UnityEngine;
using Zenject;

public class GridSpaceController : IInitializable
{
    private readonly GridSpace gridSpace;

    public GridSpaceController(GridSpace gridSpace)
    {
        this.gridSpace = gridSpace;
    }

    public void Initialize()
    {
        gridSpace.Init();
    }

    public void SetGridData(GridData data) => gridSpace.SetData(data);

    public void DrawSpace() => gridSpace.DrawSpace();
    public void RemoveSmallAreas() => gridSpace.RemoveSmallAreas();

    public void ChangeGridActiveState() => gridSpace.gameObject.SetActive(false);

    public Vector2 GetAlignCellPos(Vector2 position) => gridSpace.GetAlignCellPos(position);
    public Vector2 GetPosByCoordinates(Vector2Int coord) => gridSpace.GetPosByCoordinates(coord);
    public Cell GetCellByPos(Vector2 position) => gridSpace.GetCellByPos(position);
}
