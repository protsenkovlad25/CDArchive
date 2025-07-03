using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : IInitializable, IFixedTickable
{
    private readonly Unit player;
    private readonly SaveController saveCntr;
    private readonly InputController inputCntr;
    private readonly GridSpaceController gridSpaceCntr;

    private PlayerData _playerData;
    private List<Cell> _filledCells;
    private bool _isFilling;

    public PlayerData PlayerData => _playerData;

    public PlayerController(Unit player, SaveController saveCntr, InputController inputCntr, GridSpaceController gridSpaceCntr)
    {
        this.player = player;
        this.saveCntr = saveCntr;
        this.inputCntr = inputCntr;
        this.gridSpaceCntr = gridSpaceCntr;
    }

    public void Initialize()
    {
        inputCntr.OnMoveInput += MoveHandler;

        _filledCells = new();

        _playerData = saveCntr.LoadPlayerData();
        CheckLoadedFiles();

        player.OnTriggerEnter += TriggerEnter;
        player.OnTriggerExit += TriggerExit;
        player.Init();
        player.StopMove();
    }

    private void CheckLoadedFiles()
    {
        List<GameFileConfig> configFiles = Configs.GlobalSettings.GameFileSettings;
        if (_playerData.GameFiles.Count < configFiles.Count)
        {
            int startIndex = _playerData.GameFiles.Count;
            int count = configFiles.Count;
            for (int i = startIndex; i < count; i++)
            {
                _playerData.GameFiles.Add(new GameFileData(configFiles[i].GameFileData));
            }
        }
        saveCntr.SavePlayerData(_playerData);
    }
    public void SaveData()
    {
        saveCntr.SavePlayerData(_playerData);
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

        if (cellOnPos != null)
        {
            if (cellOnPos.State == CellState.Internal)
            {
                cellOnPos.ChangeState(CellState.Filled);
                _filledCells.Add(cellOnPos);

                if (!_isFilling)
                    StartFilling();
            }
            else if (cellOnPos.State == CellState.External && _isFilling)
            {
                gridSpaceCntr.RemoveSmallAreas(_filledCells);
                EndFilling();
            }
        }
    }

    public void StartPlayer()
    {
        player.StartMove();
    }
    public void StopPlayer()
    {
        if (_isFilling)
            EndFilling();

        player.StopMove();
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
