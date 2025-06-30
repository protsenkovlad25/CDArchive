using UnityEngine;

public class GameplayController
{
    private readonly InterfaceController interfaceController;

    public GameplayController(InterfaceController interfaceController)
    {
        this.interfaceController = interfaceController;
    }

    public void Init()
    {
        interfaceController.Screens.GameScreen.OnPauseClicked += PauseGame;
        interfaceController.Screens.PauseScreen.OnAbortClicked += AbortGame;
        interfaceController.Screens.PauseScreen.OnContinueClicked += ContinueGame;
    }

    private void StartGame()
    {
        ChangeTimeScale(1f);

        // start code
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
    }

    private void ChangeTimeScale(float scale)
    {
        Time.timeScale = scale;

        Debug.Log($"TimeScale changed to - {scale}");
    }
}
