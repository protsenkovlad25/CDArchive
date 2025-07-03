using UnityEngine;
using Zenject;

public class GameplayController : IInitializable
{
    private readonly InterfaceController interfaceCntr;
    private readonly GridSpaceController gridSpaceCntr;
    private readonly PlayerController playerCntr;
    private readonly GameView gameView;

    private readonly GameScreen gameScreen;
    private readonly PauseScreen pauseScreen;

    public GameplayController(InterfaceController interfaceCntr, GridSpaceController gridSpaceCntr,
        PlayerController playerCntr, GameView gameView)
    {
        this.interfaceCntr = interfaceCntr;
        this.gridSpaceCntr = gridSpaceCntr;
        this.playerCntr = playerCntr;
        this.gameView = gameView;

        gameScreen = interfaceCntr.Screens.GameScreen;
        pauseScreen = interfaceCntr.Screens.PauseScreen;
    }

    public void Initialize()
    {
        gameScreen.OnOpened += StartGame;
        gameScreen.OnPauseClicked += PauseGame;
        pauseScreen.OnAbortClicked += AbortGame;
        pauseScreen.OnContinueClicked += ContinueGame;

        gridSpaceCntr.SetGridData(Configs.GlobalSettings.LevelSettings[0].GridData);
        gameView.ChangeActiveState(false);
    }

    private void StartGame()
    {
        ChangeTimeScale(1f);
        gridSpaceCntr.DrawSpace();
        gameView.ChangeActiveState(true);
        playerCntr.SetPlayerPos(gridSpaceCntr.GetPosByCoordinates(Configs.GlobalSettings.LevelSettings[0].PlayerStartPos));
        playerCntr.StartPlayer();
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
    }
    private void RestartGame()
    {
        StopGame();
        StartGame();
    }
    private void AbortGame()
    {
        StopGame();
        ChangeTimeScale(1f);
        gameView.ChangeActiveState(false);
    }

    private void ChangeTimeScale(float scale)
    {
        Time.timeScale = scale;

        Debug.Log($"TimeScale changed to - {scale}");
    }
}
