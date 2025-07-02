using UnityEngine;

[System.Serializable]
public class GridData
{
    [SerializeField] private Vector2Int _internalSize;
    [SerializeField] private Vector2Int _externalLBPoint;
    [SerializeField] private Vector2Int _externalRTPoint;

    public GridData(GridData data)
    {
        _internalSize = data.InternalSize;
        _externalLBPoint = data.ExternalLBPoint;
        _externalRTPoint = data.ExternalRTPoint;
    }

    public Vector2Int InternalSize { get => _internalSize; set => _internalSize = value; }
    public Vector2Int ExternalLBPoint { get => _externalLBPoint; set => _externalLBPoint = value; }
    public Vector2Int ExternalRTPoint { get => _externalRTPoint; set => _externalRTPoint = value; }
}
