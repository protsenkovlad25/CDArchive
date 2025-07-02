using UnityEngine;

public interface IMovement
{
    void Move(Rigidbody2D rb, Vector2 direction);
    void StartMove();
    void StopMove();
}
