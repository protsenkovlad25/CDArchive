using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private ScreensView _screens;
    [SerializeField] private GameView _gameView;
    [SerializeField] private Unit _player;

    public override void InstallBindings()
    {
        SaveController saveCntr = new();
        InterfaceController interfaceCntr = new(_screens);
        InputController inputCntr = new();
        GridSpaceController gridSpaceCntr = new(_gameView.GameSpace);
        PlayerController playerCntr = new(_gameView.Player, saveCntr, inputCntr, gridSpaceCntr);
        ArchiveController archiveCntr = new(interfaceCntr, playerCntr);
        GameplayController gameplayCntr = new(interfaceCntr, gridSpaceCntr, archiveCntr, playerCntr, _gameView);
        GameController gameCntr = new(interfaceCntr);

        Container.BindInterfacesAndSelfTo<SaveController>().FromInstance(saveCntr).AsSingle();
        Container.BindInterfacesAndSelfTo<InputController>().FromInstance(inputCntr).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerController>().FromInstance(playerCntr).AsSingle();
        Container.BindInterfacesAndSelfTo<GridSpaceController>().FromInstance(gridSpaceCntr).AsSingle();
        Container.BindInterfacesAndSelfTo<InterfaceController>().FromInstance(interfaceCntr).AsSingle();
        Container.BindInterfacesAndSelfTo<GameplayController>().FromInstance(gameplayCntr).AsSingle();
        Container.BindInterfacesAndSelfTo<ArchiveController>().FromInstance(archiveCntr).AsSingle();
        Container.BindInterfacesAndSelfTo<GameController>().FromInstance(gameCntr).AsSingle();

        Container.Bind<ScreensView>().FromInstance(_screens).AsSingle();
        Container.Bind<GameView>().FromInstance(_gameView).AsSingle();

        Container.BindInterfacesAndSelfTo<Unit>().FromInstance(_player).AsSingle();
    }
}
