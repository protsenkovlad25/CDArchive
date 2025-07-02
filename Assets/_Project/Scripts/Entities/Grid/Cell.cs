using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum CellState { Internal, External, Filled }

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private CellState _state;
    [SerializeField] private SerializedDictionary<CellState, string> _layerByState;

    public void Init()
    {
        InternalState();
    }

    public void Reuse()
    {
        InternalState();
    }

    #region StateMachine

    #region States
    private void InternalState()
    {
        _state = CellState.Internal;
        SetLayerByState();
        
        _renderer.color = Color.cyan;
    }
    private void ExternalState()
    {
        _state = CellState.External;
        SetLayerByState();

        _renderer.color = Color.black;
    }
    private void FilledState()
    {
        _state = CellState.Filled;
        SetLayerByState();

        _renderer.color = Color.yellow;
    }
    #endregion

    private void SetLayerByState()
    {
        gameObject.layer = LayerMask.NameToLayer(_layerByState[_state]);
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
