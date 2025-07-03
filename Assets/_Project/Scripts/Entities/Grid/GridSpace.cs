using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VP;

public class GridSpace : MonoBehaviour
{
    public event UnityAction OnUpdatedSpace;

    [Header("Objects")]
    [SerializeField] private Transform _cellParent;
    [SerializeField] private Transform _startCellParent;
    [Header("Grid Data")]
    [SerializeField] private GridData _data;
    [SerializeField] private GridBorders _borders;
    [Header("Grid Settings")]
    [SerializeField] private GridSettingsConfig _config;

    private int _startInternalCells;
    private int _currentInternalCells;

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
        _cellsByPos = new();
        _parents = new();

        _startInternalCells = 0;
        _currentInternalCells = 0;

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
                cell.SetCoordPos(gridPos);
                
                if (x >= internalLB.x && x <= internalRT.x &&
                    y >= internalLB.y && y <= internalRT.y)
                {
                    _startInternalCells++;
                    _currentInternalCells++;
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
        _startInternalCells = 0;
        _currentInternalCells = 0;

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

        Cell cell = null;
        if (_cellsByPos.ContainsKey(coordPos))
            cell = _cellsByPos[coordPos];

        return cell;
    }
    public List<Cell> GetStateCellsNearCell(Cell cell, CellState state)
    {
        Vector2Int pos = cell.CoordPos;
        List<Cell> cells = new();

        if (_cellsByPos[new Vector2Int(pos.x, pos.y + 1)].State == state) cells.Add(_cellsByPos[new Vector2Int(pos.x, pos.y + 1)]);
        if (_cellsByPos[new Vector2Int(pos.x, pos.y - 1)].State == state) cells.Add(_cellsByPos[new Vector2Int(pos.x, pos.y + 1)]);
        if (_cellsByPos[new Vector2Int(pos.x + 1, pos.y)].State == state) cells.Add(_cellsByPos[new Vector2Int(pos.x, pos.y + 1)]);
        if (_cellsByPos[new Vector2Int(pos.x - 1, pos.y)].State == state) cells.Add(_cellsByPos[new Vector2Int(pos.x, pos.y + 1)]);
        
        return cells;
    }
    public float GetInternalCellPercent()
    {
        return (float)_currentInternalCells / _startInternalCells;
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

    #region Methods for Remove Areas
    public List<List<Vector2Int>> FindAreas()
    {
        List<List<Vector2Int>> areas = new();
        HashSet<Vector2Int> visited = new();

        List<Vector2Int> areaCells;
        foreach (var pos in _cellsByPos.Keys)
        {
            if (_cellsByPos[pos].State == CellState.Internal && !visited.Contains(pos))
            {
                areaCells = DFS(pos, visited);
                areas.Add(areaCells);
            }
        }

        return areas;
    }
    private List<Vector2Int> DFS(Vector2Int start, HashSet<Vector2Int> visited)
    {
        Stack<Vector2Int> stack = new();
        List<Vector2Int> areaCells = new();
        stack.Push(start);
        visited.Add(start);

        Vector2Int cellPos;
        while (stack.Count > 0)
        {
            cellPos = stack.Pop();
            areaCells.Add(cellPos);

            foreach (var neighbor in GetNeighbors(cellPos))
            {
                if (visited.Contains(neighbor)) continue;

                if (_cellsByPos.ContainsKey(neighbor) && _cellsByPos[neighbor].State == CellState.Internal)
                {
                    visited.Add(neighbor);
                    stack.Push(neighbor);
                }
            }
        }

        return areaCells;
    }
    private List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        List<Vector2Int> neighbors = new();
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0)
        };

        Vector2Int neighborPos;
        foreach (var dir in directions)
        {
            neighborPos = position + dir;
            neighbors.Add(neighborPos);
        }

        return neighbors;
    }
    public void RemoveSmallAreas(List<Cell> filledCells)
    {
        List<List<Vector2Int>> areas = FindAreas();

        areas.Sort((a, b) => a.Count.CompareTo(b.Count));

        List<Vector2Int> largestArea = areas[areas.Count - 1];

        foreach (var area in areas.GetRange(0, areas.Count - 1))
        {
            foreach (var cellPos in area)
            {
                if (_cellsByPos.ContainsKey(cellPos))
                {
                    _cellsByPos[cellPos].ChangeState(CellState.External);
                    _currentInternalCells--;
                }
            }
        }

        foreach (var cell in filledCells)
        {
            cell.ChangeState(CellState.External);
            _currentInternalCells--;
        }

        OnUpdatedSpace?.Invoke();
    }
    #endregion
}
