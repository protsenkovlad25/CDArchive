using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum CellState { Internal, External, Filled }

public class Cell : MonoBehaviour
{
    [System.Serializable]
    private class StateData
    {
        public string Layer;
        public Color Color;
    }

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private CellState _state;
    [SerializeField] private SerializedDictionary<CellState, StateData> _dataByState;

    private Vector2Int _coordPos;

    public Vector2Int CoordPos => _coordPos;
    public CellState State => _state;
    public float Size => _renderer.transform.localScale.x;

    public void Init()
    {
        ExternalState();
    }

    public void Reuse()
    {
        ExternalState();
    }

    public void SetCoordPos(Vector2Int pos)
    {
        _coordPos = pos;
    }

    #region StateMachine

    #region States
    private void InternalState()
    {
        _state = CellState.Internal;
        SetDataByState();
    }
    private void ExternalState()
    {
        _state = CellState.External;
        SetDataByState();
    }
    private void FilledState()
    {
        _state = CellState.Filled;
        SetDataByState();
    }
    #endregion

    private void SetDataByState()
    {
        gameObject.layer = LayerMask.NameToLayer(_dataByState[_state].Layer);
        _renderer.color = _dataByState[_state].Color;
    }

    public void ChangeState(CellState state)
    {
        NewState(state);
    }
    private void NewState(CellState state)
    {
        switch (state)
        {
            case CellState.Internal: InternalState(); break;
            case CellState.External: ExternalState(); break;
            case CellState.Filled: FilledState(); break;
        }
    }
    #endregion
}
