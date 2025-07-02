using UnityEngine;

public class PlayerController
{
    private readonly PlayerUnit player;
    private readonly InputController inputController;

    public PlayerController(PlayerUnit player, InputController inputController)
    {
        this.player = player;
        this.inputController = inputController;


    }

    private void MoveHandler(Vector2Int direction)
    {

    }
}
