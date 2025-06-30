using System.Collections.Generic;

public class InterfaceController
{
    private readonly ScreensView screensView;

    private List<Screen> _screens => screensView.Screens;
    public ScreensView Screens => screensView;

    public InterfaceController(ScreensView screensView)
    {
        this.screensView = screensView;
    }

    #region Methods
    public void Init()
    {
        foreach (var screen in _screens)
            screen.Init();

        screensView.MenuScreen.OnArchiveClicked += () => OpenScreen(screensView.ArchiveScreen, true);
        screensView.ArchiveScreen.OnMenuClicked += () => OpenScreen(screensView.MenuScreen, true);
        screensView.ArchiveScreen.OnCompressClicked += () => OpenScreen(screensView.GameScreen, true);
        screensView.GameScreen.OnPauseClicked += () => OpenScreen(screensView.PauseScreen);
        screensView.PauseScreen.OnContinueClicked += () => CloseScreen(screensView.PauseScreen);
        screensView.PauseScreen.OnAbortClicked += () => OpenScreen(screensView.ArchiveScreen, true);
    }

    #region Open Methods
    public void OpenScreen(Screen screen, bool isCloseOther = false)
    {
        if (isCloseOther)
            CloseAllOpenedScreens();

        screen.Open();
    }
    #endregion

    #region Close Methods
    public void CloseScreen(Screen screen)
    {
        screen.Close();
    }
    public void CloseAllOpenedScreens()
    {
        foreach (var screen in screensView.Screens)
        {
            if (screen.gameObject.activeSelf)
            {
                CloseScreen(screen);
            }
        }
    }
    #endregion

    #endregion
}
