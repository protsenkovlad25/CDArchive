using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private ScreensView _screens;
    [SerializeField] private GameView _gameView;

    public override void InstallBindings()
    {
        InputController input = new();

        InterfaceController interfaceContr = new(_screens);
        GameplayController gameplay = new(interfaceContr, _gameView);
        ArchiveController archive = new(interfaceContr);
        GameController game = new(interfaceContr);

        interfaceContr.Init();
        archive.Init();
        gameplay.Init();
        game.Init();
    }
}
