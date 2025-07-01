using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private ScreensView _screens;

    public override void InstallBindings()
    {
        InterfaceController interfaceContr = new(_screens);
        GameplayController gameplay = new(interfaceContr);
        ArchiveController archive = new(interfaceContr);
        GameController game = new(interfaceContr);

        interfaceContr.Init();
        archive.Init();
        gameplay.Init();
        game.Init();
    }
}
