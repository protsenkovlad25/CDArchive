using System.Collections.Generic;
using UnityEngine;
using VP;

public class GridSpace : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Transform _cellParent;
    [SerializeField] private Transform _startCellParent;
    [Header("Grid Data")]
    [SerializeField] private GridData _data;
    [SerializeField] private GridBorders _borders;
    [Header("Grid Settings")]
    [SerializeField] private float _spacing;
    [SerializeField] private Vector2 _offset;

    private Dictionary<Vector2Int, Cell> _cellsByPos;
    private List<GameObject> _parents;

    private Pool<Cell> _poolCells;

    private void OnValidate()
    {
        if (_cellsByPos != null)
        {
            UpdateCellPositions();
            _borders.UpdateBorders(_data.InternalSize, _spacing, _offset);
        }
    }

    [ContextMenu("Init")]
    public void Init()
    {
        _poolCells = new Pool<Cell>(_cellPrefab, _startCellParent, 2100);
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

        int width = _data.InternalSize.x;
        int height = _data.InternalSize.y;
        Vector2Int extLB = _data.ExternalLBPoint;
        Vector2Int extRT = _data.ExternalRTPoint;

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
                cell.Init();
                cell.Reuse();
                if (x >= extLB.x && x <= extRT.x &&
                    y >= extLB.y && y <= extRT.y)
                {
                    cell.ChangeState(CellState.External);
                }
                _cellsByPos[gridPos] = cell;
            }
        }

        UpdateCellPositions();
        _borders.UpdateBorders(_data.InternalSize, _spacing, _offset);
    }

    [ContextMenu("Clear Space")]
    public void ClearSpace()
    {
        if (_cellsByPos == null) return;

        _poolCells.ReturnAll();

        _cellsByPos.Clear();
    }

    private void UpdateCellPositions()
    {
        float offsetX = (_data.InternalSize.x - 1) * _spacing / 2f;
        float offsetY = (_data.InternalSize.y - 1) * _spacing / 2f;
        Vector3 centerOffset = new(offsetX, offsetY, 0f);
        Vector3 extraOffset = new(_offset.x, _offset.y, 0f);
        
        Cell cell;
        Vector3 pos;
        Vector2Int gridPos;
        foreach (var kvp in _cellsByPos)
        {
            gridPos = kvp.Key;
            cell = kvp.Value;

            pos = new(gridPos.x * _spacing, gridPos.y * _spacing, 0f);
            cell.transform.localPosition = pos - centerOffset + extraOffset;
        }
    }
}
