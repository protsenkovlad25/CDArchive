using UnityEngine;

public enum CellState { Internal, External, Filled }

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private CellState _state;

    public void Init()
    {
        _renderer.color = Color.white;
    }

    public void Reuse()
    {
        InternalState();
    }

    #region StateMachine

    #region States
    private void InternalState()
    {
        _renderer.color = Color.cyan;
    }
    private void ExternalState()
    {
        _renderer.color = Color.black;
    }
    private void FilledState()
    {
        _renderer.color = Color.yellow;
    }
    #endregion

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
