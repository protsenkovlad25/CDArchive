using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private ScreensView _screens;

    public override void InstallBindings()
    {
        InterfaceController interfaceContr = new(_screens);
        GameplayController gameplay = new(interfaceContr);
        ArchiveController archive = new();
        GameController game = new();

        interfaceContr.Init();
        gameplay.Init();
    }
}
