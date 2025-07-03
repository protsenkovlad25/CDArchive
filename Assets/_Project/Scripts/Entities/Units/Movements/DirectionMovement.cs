using UnityEngine;
using Zenject;

[System.Serializable]
public class DirectionMovement : Movement
{
    [Inject] private GridSpaceController _gridCntr;

    public override void Move(Vector2 direction)
    {
        if (_isStop) return;

        if (direction != Vector2.zero)
        {
            _t.position = _gridCntr.GetAlignCellPos(_t.position);

            _direction = direction;
        }
    }

    public override void Update()
    {
        base.Update();

        if (_direction == Vector2.zero) return;

        _rb.MovePosition(_rb.position + _direction * _speed * Time.deltaTime);
    }

    public override void StopMove()
    {
        base.StopMove();

        _rb.linearVelocity = Vector2.zero;
        _direction = Vector2.zero;
    }
}
