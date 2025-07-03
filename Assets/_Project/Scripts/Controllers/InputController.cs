using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class InputController : IInitializable, ITickable
{
    public event UnityAction<Vector2Int> OnMoveInput;

    public InputController()
    {

    }

    public void Initialize()
    {
    }

    public void Tick()
    {
        UpdateMove();
    }

    private void UpdateMove()
    {
        Vector2Int direction = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.W)) direction = Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.S)) direction = Vector2Int.down;
        if (Input.GetKeyDown(KeyCode.A)) direction = Vector2Int.left;
        if (Input.GetKeyDown(KeyCode.D)) direction = Vector2Int.right;

        if (direction != Vector2Int.zero)
            OnMoveInput?.Invoke(direction);
    }
}
