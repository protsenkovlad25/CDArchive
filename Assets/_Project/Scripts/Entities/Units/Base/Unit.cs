using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Unit : MonoBehaviour, IMoving
{
    [SerializeField] private Rigidbody2D _rb;

    private Movement _movement;

    public abstract void Init();

    public virtual void Move(Vector2 direction)
    {
        _movement.Move(_rb, direction);
    }
    public virtual void StartMove()
    {
        _movement.StartMove();
    }
    public virtual void StopMove()
    {
        _movement.StopMove();
    }
}
