using UnityEngine;

[System.Serializable]
public class GridData
{
    [SerializeField] private Vector2Int _externalSize;
    [SerializeField] private Vector2Int _internalLBPoint;
    [SerializeField] private Vector2Int _internalRTPoint;

    public GridData(GridData data)
    {
        _externalSize = data.ExternalSize;
        _internalLBPoint = data.InternalLBPoint;
        _internalRTPoint = data.InternalRTPoint;
    }

    public Vector2Int ExternalSize { get => _externalSize; set => _externalSize = value; }
    public Vector2Int InternalLBPoint { get => _internalLBPoint; set => _internalLBPoint = value; }
    public Vector2Int InternalRTPoint { get => _internalRTPoint; set => _internalRTPoint = value; }
}
