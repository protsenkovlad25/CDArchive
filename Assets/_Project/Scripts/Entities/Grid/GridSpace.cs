using System.Collections.Generic;
using UnityEngine;
using VP;

public class GridSpace : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform _cellParent;
    [SerializeField] private Transform _startCellParent;
    [Header("Grid Data")]
    [SerializeField] private GridData _data;
    [SerializeField] private GridBorders _borders;
    [Header("Grid Settings")]
    [SerializeField] private GridSettingsConfig _config;

    private Dictionary<Vector2Int, Cell> _cellsByPos;
    private List<GameObject> _parents;

    private Pool<Cell> _poolCells;

    private void OnValidate()
    {
        if (_cellsByPos != null)
        {
            UpdateCellPositions();
            _borders.UpdateBorders(_data.ExternalSize, _config.Spacing, _config.Offset);
        }
    }

    [ContextMenu("Init")]
    public void Init()
    {
        _poolCells = new Pool<Cell>(_config.CellPrefab, _startCellParent, 2100);
        _poolCells.OnCreateNew += NewCell;
        
        foreach (var cell in _poolCells.ObjectsList)
            NewCell(cell);
    }
    private void NewCell(Cell newCell)
    {
        newCell.Init();
    }

    public void SetData(GridData data)
    {
        _data = new GridData(data);
    }

    [ContextMenu("Draw Space")]
    public void DrawSpace()
    {
        ClearSpace();

        _cellsByPos = new();
        _parents = new();

        int width = _data.ExternalSize.x;
        int height = _data.ExternalSize.y;
        Vector2Int internalLB = _data.InternalLBPoint;
        Vector2Int internalRT = _data.InternalRTPoint;

        Cell cell;
        Vector2Int gridPos;
        GameObject parent;
        for (int x = 0; x < width; x++)
        {
            if (_parents.Count < width)
            {
                parent = new GameObject();
                parent.name = $"Col_{x}";
                parent.transform.parent = _cellParent;
                _parents.Add(parent);
            }
            else parent = _parents[x];

            for (int y = 0; y < height; y++)
            {
                gridPos = new(x, y);

                cell = _poolCells.Take();
                cell.transform.parent = parent.transform;
                cell.name = $"Cell_C{x}_R{y}";
                cell.Reuse();
                if (x >= internalLB.x && x <= internalRT.x &&
                    y >= internalLB.y && y <= internalRT.y)
                {
                    cell.ChangeState(CellState.Internal);
                }
                _cellsByPos[gridPos] = cell;
            }
        }

        UpdateCellPositions();
        _borders.UpdateBorders(_data.ExternalSize, _config.Spacing, _config.Offset);
    }

    [ContextMenu("Clear Space")]
    public void ClearSpace()
    {
        if (_cellsByPos == null) return;

        _poolCells.ReturnAll();

        _cellsByPos.Clear();
    }

    public Vector2 GetPosByCoordinates(Vector2Int coord)
    {
        return _cellsByPos[coord].transform.position;
    }
    public Vector2Int GetCoordPosByVectorPos(Vector2 position)
    {
        float spacing = _config.Spacing;
        Vector2 offset = _config.Offset;

        float offsetX = (_data.ExternalSize.x - 1) * spacing / 2f;
        float offsetY = (_data.ExternalSize.y - 1) * spacing / 2f;
        Vector3 centerOffset = new(offsetX, offsetY, 0f);
        Vector3 extraOffset = new(offset.x, offset.y, 0f);

        int x = Mathf.RoundToInt((position.x + centerOffset.x - extraOffset.x) / spacing);
        int y = Mathf.RoundToInt((position.y + centerOffset.y - extraOffset.y) / spacing);

        return new Vector2Int(x, y);
    }
    public Vector2 GetAlignCellPos(Vector2 position)
    {
        Vector2Int coordPos = GetCoordPosByVectorPos(position);
        return _cellsByPos[coordPos].transform.position;
    }
    public Cell GetCellByPos(Vector2 position)
    {
        Vector2Int coordPos = GetCoordPosByVectorPos(position);

        return _cellsByPos[coordPos];
    }

    private void UpdateCellPositions()
    {
        float spacing = _config.Spacing;
        Vector2 offset = _config.Offset;

        float offsetX = (_data.ExternalSize.x - 1) * spacing / 2f;
        float offsetY = (_data.ExternalSize.y - 1) * spacing / 2f;
        Vector3 centerOffset = new(offsetX, offsetY, 0f);
        Vector3 extraOffset = new(offset.x, offset.y, 0f);
        
        Cell cell;
        Vector3 pos;
        Vector2Int gridPos;
        foreach (var kvp in _cellsByPos)
        {
            gridPos = kvp.Key;
            cell = kvp.Value;

            pos = new(gridPos.x * spacing, gridPos.y * spacing, 0f);
            cell.transform.localPosition = pos - centerOffset + extraOffset;
        }
    }
}
