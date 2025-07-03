using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : IInitializable, IFixedTickable
{
    private readonly Unit player;
    private readonly InputController inputCntr;
    private readonly GridSpaceController gridSpaceCntr;

    private List<Cell> _filledCells;
    private bool _isFilling;

    public PlayerController(Unit player, InputController inputCntr, GridSpaceController gridSpaceCntr)
    {
        this.player = player;
        this.inputCntr = inputCntr;
        this.gridSpaceCntr = gridSpaceCntr;
    }

    public void Initialize()
    {
        inputCntr.OnMoveInput += MoveHandler;

        _filledCells = new();

        player.OnTriggerEnter += TriggerEnter;
        player.OnTriggerExit += TriggerExit;
        player.Init();
    }

    public void FixedTick()
    {
        CheckPlayerPos();
    }

    public void SetPlayerPos(Vector2 pos)
    {
        player.transform.position = pos;
    }

    public void CheckPlayerPos()
    {
        Cell cellOnPos = gridSpaceCntr.GetCellByPos(player.transform.position);
        
        if (cellOnPos.State == CellState.Internal)
        {
            cellOnPos.ChangeState(CellState.Filled);
            _filledCells.Add(cellOnPos);

            if (!_isFilling)
                StartFilling();
        }
        else if (cellOnPos.State == CellState.External && _isFilling)
        {
            EndFilling();
        }
    }

    private void MoveHandler(Vector2Int direction)
    {
        player.Move(direction);
    }
    
    private void StartFilling()
    {
        _isFilling = true;
    }
    private void EndFilling()
    {
        _isFilling = false;

        player.StopMove();
        player.StartMove();
        player.transform.position = gridSpaceCntr.GetAlignCellPos(player.transform.position);

        foreach (var cell in _filledCells)
            cell.ChangeState(CellState.External);

        _filledCells.Clear();
    }

    private void TriggerEnter(Collider2D collider)
    {
        //if (collider.TryGetComponent(out Cell cell))
        //{
        //    cell.ChangeState(CellState.Filled);
        //    _filledCells.Add(cell);
        //}
    }
    private void TriggerExit(Collider2D collider)
    {
    }
}
