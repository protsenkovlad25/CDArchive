using UnityEngine;
using Zenject;

public class GameplayController : IInitializable
{
    private readonly InterfaceController interfaceCntr;
    private readonly GridSpaceController gridSpaceCntr;
    private readonly ArchiveController archiveCntr;
    private readonly EnemiesController enemiesCntr;
    private readonly PlayerController playerCntr;
    private readonly GameView gameView;

    private readonly GameScreen gameScreen;
    private readonly PauseScreen pauseScreen;

    private int _currentLevel;
    private int _currentHealths;
    private int _completedLevels;
    private GlobalSettingsConfig _globalConfig;
    private LevelSettingsConfig _currentLevelConfig;

    public GameplayController(InterfaceController interfaceCntr, GridSpaceController gridSpaceCntr,
        ArchiveController archiveCntr, EnemiesController enemiesCntr,
        PlayerController playerCntr, GameView gameView)
    {
        this.interfaceCntr = interfaceCntr;
        this.gridSpaceCntr = gridSpaceCntr;
        this.archiveCntr = archiveCntr;
        this.enemiesCntr = enemiesCntr;
        this.playerCntr = playerCntr;
        this.gameView = gameView;

        gameScreen = interfaceCntr.Screens.GameScreen;
        pauseScreen = interfaceCntr.Screens.PauseScreen;
    }

    public void Initialize()
    {
        _globalConfig = Configs.GlobalSettings;

        gameScreen.OnOpened += StartLevel;
        gameScreen.OnPauseClicked += PauseGame;
        pauseScreen.OnAbortClicked += AbortGame;
        pauseScreen.OnContinueClicked += ContinueGame;

        gridSpaceCntr.OnUpdatedSpace += UpdateProgress;
        gridSpaceCntr.OnUpdatedSpace += CheckComplete;

        playerCntr.Player.OnCollisionEnter += CheckCollision;

        gameView.ChangeActiveState(false);
    }

    private void StartLevel()
    {
        _currentLevel = 1;
        _completedLevels = 0;
        _currentLevelConfig = _globalConfig.LevelSettings[_currentLevel - 1];

        StartGame();
    }
    private void NextLevel()
    {
        if (_currentLevel < _globalConfig.LevelSettings.Count)
        {
            _currentLevel++;
            _currentLevelConfig = _globalConfig.LevelSettings[_currentLevel - 1];

            StartGame();
        }
        else EndGame();
    }
    private void StartGame()
    {
        ChangeTimeScale(1f);
        
        gridSpaceCntr.SetGridData(_currentLevelConfig.GridData);
        gridSpaceCntr.DrawSpace();

        _currentHealths = _currentLevelConfig.PlayerHealths;
        gameScreen.ActivateHealths(_currentLevelConfig.PlayerHealths);

        Unit enemy = enemiesCntr.SpawnEnemy(EnemyType.ShootEnemy, gridSpaceCntr.GetPosByCoordinates(_currentLevelConfig.EnemyStartPos));
        enemy.Attack.ShootInterval = _currentLevelConfig.ShootInterval;
        enemy.StartBehavior();

        playerCntr.SetPlayerPos(gridSpaceCntr.GetPosByCoordinates(_currentLevelConfig.PlayerStartPos));
        playerCntr.StartPlayer();
        
        gameView.ChangeActiveState(true);
        
        UpdateProgress();
    }
    private void PauseGame()
    {
        ChangeTimeScale(0f);
    }
    private void ContinueGame()
    {
        ChangeTimeScale(1f);
    }
    private void StopGame()
    {
        playerCntr.StopPlayer();
        enemiesCntr.DeactiveAllEnemies();
    }
    private void AbortGame()
    {
        StopGame();
        ChangeTimeScale(1f);
        gameView.ChangeActiveState(false);
    }
    private void EndGame()
    {
        StopGame();
        ChangeTimeScale(1f);
        gameView.ChangeActiveState(false);

        archiveCntr.UpdateSelectedFileCompression(_completedLevels);
        interfaceCntr.OpenScreen(typeof(ArchiveScreen), true);
    }

    private void CheckCollision(Unit player, Collision2D collsiion)
    {
        if (collsiion.collider.TryGetComponent<Unit>(out _))
            LostHealth();
    }
    private void LostHealth()
    {
        if (_currentHealths - 1 != 0)
        {
            _currentHealths--;
            playerCntr.StopPlayer(true);
            playerCntr.StartPlayer();
            gameScreen.RemoveHealth();
        }
        else EndGame();
    }

    private void CheckComplete()
    {
        if (1 - gridSpaceCntr.GetInternalCellPercent() >= _currentLevelConfig.CompletePercent)
        {
            CompleteLevel();
        }
    }
    private void CompleteLevel()
    {
        _completedLevels++;

        StopGame();
        NextLevel();
    }

    private void ChangeTimeScale(float scale)
    {
        Time.timeScale = scale;

        Debug.Log($"TimeScale changed to - {scale}");
    }

    private void UpdateProgress()
    {
        gameScreen.UpdateProgress(1 - gridSpaceCntr.GetInternalCellPercent());
    }
}
