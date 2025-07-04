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
        CheckLoadedData();

        player.OnTriggerEnter += TriggerEnter;
        player.OnTriggerExit += TriggerExit;
        player.Init();
        player.StopMove();
    }

    private void CheckLoadedData()
    {
        List<GameFileData> gameFiles = _playerData.GameFiles;
        List<GameFileConfig> configFiles = Configs.GlobalSettings.GameFileSettings;
        if (gameFiles.Count < configFiles.Count)
        {
            int startIndex = gameFiles.Count;
            int count = configFiles.Count;
            for (int i = startIndex; i < count; i++)
            {
                gameFiles.Add(new GameFileData(configFiles[i].GameFileData));
            }
        }

        for (int i = gameFiles.Count - 1; i >= 0; i--)
        {
            if (_playerData.DiscData.Files.Exists(f => f.Id == gameFiles[i].Id))
            {
                gameFiles.Remove(gameFiles[i]);
            }
        }

        SaveData();
    }
    public void SaveData()
    {
        saveCntr.SavePlayerData(_playerData);
    }
    public void WriteFileOnDisc(GameFileData file)
    {
        _playerData.DiscData.AddFile(file);
    }
    public bool CanWriteFile(GameFileData file)
    {
        return file.Size + _playerData.DiscData.UsedSpace <= _playerData.DiscData.Capacity;
    }
    public bool IsDiscFilled()
    {
        DiscData data = _playerData.DiscData;
        return data.UsedSpace / data.Capacity > 0.95f;
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
