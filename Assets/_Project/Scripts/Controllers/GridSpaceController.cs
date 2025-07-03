using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GridSpaceController : IInitializable
{
    public event UnityAction OnUpdatedSpace;

    private readonly GridSpace gridSpace;

    public GridSpaceController(GridSpace gridSpace)
    {
        this.gridSpace = gridSpace;
    }

    public void Initialize()
    {
        gridSpace.OnUpdatedSpace += () => OnUpdatedSpace?.Invoke();
        gridSpace.Init();
    }

    public void SetGridData(GridData data) => gridSpace.SetData(data);
    public float GetInternalCellPercent() => gridSpace.GetInternalCellPercent();

    public void DrawSpace() => gridSpace.DrawSpace();
    public void RemoveSmallAreas(List<Cell> filledCells) => gridSpace.RemoveSmallAreas(filledCells);

    public void ChangeGridActiveState() => gridSpace.gameObject.SetActive(false);

    public Vector2 GetAlignCellPos(Vector2 position) => gridSpace.GetAlignCellPos(position);
    public Vector2 GetPosByCoordinates(Vector2Int coord) => gridSpace.GetPosByCoordinates(coord);
    public Cell GetCellByPos(Vector2 position) => gridSpace.GetCellByPos(position);
}
