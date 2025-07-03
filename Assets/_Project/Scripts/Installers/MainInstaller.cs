using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private ScreensView _screens;
    [SerializeField] private GameView _gameView;
    [SerializeField] private Unit _player;

    public override void InstallBindings()
    {
        InputController input = new();
        GridSpaceController gridSpace = new(_gameView.GameSpace);
        PlayerController player = new(_gameView.Player, input, gridSpace);
        InterfaceController interfaceCntr = new(_screens);
        GameplayController gameplay = new(interfaceCntr, gridSpace, player, _gameView);
        ArchiveController archive = new(interfaceCntr);
        GameController game = new(interfaceCntr);

        Container.BindInterfacesAndSelfTo<InputController>().FromInstance(input).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerController>().FromInstance(player).AsSingle();
        Container.BindInterfacesAndSelfTo<GridSpaceController>().FromInstance(gridSpace).AsSingle();
        Container.BindInterfacesAndSelfTo<InterfaceController>().FromInstance(interfaceCntr).AsSingle();
        Container.BindInterfacesAndSelfTo<GameplayController>().FromInstance(gameplay).AsSingle();
        Container.BindInterfacesAndSelfTo<ArchiveController>().FromInstance(archive).AsSingle();
        Container.BindInterfacesAndSelfTo<GameController>().FromInstance(game).AsSingle();

        Container.Bind<ScreensView>().FromInstance(_screens).AsSingle();
        Container.Bind<GameView>().FromInstance(_gameView).AsSingle();

        Container.BindInterfacesAndSelfTo<Unit>().FromInstance(_player).AsSingle();
    }
}
