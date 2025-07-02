using System.Collections.Generic;
using UnityEngine;

public class ScreensView : MonoBehaviour
{
    [SerializeField] private MenuScreen _menuScreen;
    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private PauseScreen _pauseScreen;
    [SerializeField] private ArchiveScreen _archiveScreen;

    public MenuScreen MenuScreen => _menuScreen;
    public GameScreen GameScreen => _gameScreen;
    public PauseScreen PauseScreen => _pauseScreen;
    public ArchiveScreen ArchiveScreen => _archiveScreen;

    public List<Screen> Screens => new()
    {
        _menuScreen,
        _gameScreen,
        _pauseScreen,
        _archiveScreen,
    };
}
