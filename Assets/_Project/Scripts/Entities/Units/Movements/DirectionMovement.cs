using UnityEngine;

[System.Serializable]
public class DirectionMovement : Movement
{
    public override void Move(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            _direction = direction;
        }
    }

    public override void Update()
    {
        base.Update();

        if (_direction == Vector2.zero) return;

        _rb.MovePosition(_rb.position + _direction * _speed * Time.deltaTime);

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        _t.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    public override void StopMove()
    {
        base.StopMove();

        _rb.linearVelocity = Vector2.zero;
        _direction = Vector2.zero;
    }
}
