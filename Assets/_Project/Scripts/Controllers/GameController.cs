using UnityEngine;

public class GameController
{
    private readonly InterfaceController interfaceController;

    public GameController(InterfaceController interfaceController)
    {
        this.interfaceController = interfaceController; 
    }

    public void Init()
    {
        ActivateGame();
    }

    public void ActivateGame()
    {
        interfaceController.OpenScreen(typeof(MenuScreen), true);
    }
}
