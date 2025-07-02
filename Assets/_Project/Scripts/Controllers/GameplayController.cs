using UnityEngine;

public class GameplayController
{
    private readonly InterfaceController interfaceController;
    private readonly GameView gameView;

    private readonly GameScreen gameScreen;
    private readonly PauseScreen pauseScreen;
    private readonly GridSpace gridSpace;

    public GameplayController(InterfaceController interfaceController, GameView gameView)
    {
        this.interfaceController = interfaceController;
        this.gameView = gameView;

        gridSpace = gameView.GameSpace;
        gameScreen = interfaceController.Screens.GameScreen;
        pauseScreen = interfaceController.Screens.PauseScreen;
    }

    public void Init()
    {
        gameScreen.OnOpened += StartGame;
        gameScreen.OnPauseClicked += PauseGame;
        pauseScreen.OnAbortClicked += AbortGame;
        pauseScreen.OnContinueClicked += ContinueGame;

        gridSpace.Init();
        gridSpace.DrawSpace();
        gameView.ChangeActiveState(false);
    }

    private void StartGame()
    {
        ChangeTimeScale(1f);
        gameView.ChangeActiveState(true);
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
        // stop code
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
