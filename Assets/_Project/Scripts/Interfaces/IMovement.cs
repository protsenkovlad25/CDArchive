using UnityEngine;

public interface IMovement
{
    void Init(Rigidbody2D rb, Transform t);
    void Move(Vector2 direction);
    void Update();
    void StartMove();
    void StopMove();
}
