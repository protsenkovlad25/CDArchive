using UnityEngine;

public interface IMoving
{
    void Move(Vector2 direction);
    void StartMove();
    void StopMove();
}
