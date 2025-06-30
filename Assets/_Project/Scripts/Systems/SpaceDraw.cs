using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceDraw : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Transform _cellParent;
    [Header("Internal Space")]
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [Header("External Space")]
    [SerializeField] private Vector2Int _extLBPoint;
    [SerializeField] private Vector2Int _extRTPoint;
    [Header("Grid Settings")]
    [SerializeField] private float _spacing;
    [SerializeField] private Vector2 _offset;

    private Dictionary<Vector2Int, Cell> _cellsByPos;

    private void OnValidate()
    {
        if (_cellsByPos != null)
            UpdateCellPositions();
    }

    [ContextMenu("Draw Space")]
    public void DrawSpace()
    {
        ClearSpace();

        _cellsByPos = new();

        Cell cell;
        Vector2Int gridPos;
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                gridPos = new(x, y);

                cell = Instantiate(_cellPrefab, Vector3.zero, Quaternion.identity, _cellParent);
                cell.Init();
                cell.Reuse();
                if (x >= _extLBPoint.x && x <= _extRTPoint.x &&
                    y >= _extLBPoint.y && y <= _extRTPoint.y)
                {
                    cell.ChangeState(CellState.External);
                }
                _cellsByPos[gridPos] = cell;
            }
        }

        UpdateCellPositions();
    }

    [ContextMenu("Clear Space")]
    public void ClearSpace()
    {
        if (_cellsByPos == null) return;

        for (int i = 0; i < _cellsByPos.Count; i++)
            DestroyImmediate(_cellsByPos.ElementAt(i).Value.gameObject);

        _cellsByPos.Clear();
    }

    private void UpdateCellPositions()
    {
        float offsetX = (_width - 1) * _spacing / 2f;
        float offsetY = (_height - 1) * _spacing / 2f;
        Vector3 centerOffset = new Vector3(offsetX, offsetY, 0f);
        Vector3 extraOffset = new Vector3(_offset.x, _offset.y, 0f);

        foreach (var kvp in _cellsByPos)
        {
            Vector2Int gridPos = kvp.Key;
            Cell cell = kvp.Value;

            Vector3 pos = new Vector3(gridPos.x * _spacing, gridPos.y * _spacing, 0f);
            cell.transform.localPosition = pos - centerOffset + extraOffset;
        }
    }
}
