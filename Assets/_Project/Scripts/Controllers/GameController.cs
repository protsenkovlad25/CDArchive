using UnityEngine;
using Zenject;

public class GameController : IInitializable
{
    private readonly InterfaceController interfaceController;

    public GameController(InterfaceController interfaceController)
    {
        this.interfaceController = interfaceController; 
    }

    public void Initialize()
    {
        ActivateGame();
    }

    public void ActivateGame()
    {
        interfaceController.OpenScreen(typeof(MenuScreen), true);
    }
}
