using UnityEngine;
using Zenject;

[System.Serializable]
public class GridMovement : Movement
{
    [SerializeField] private GridSettingsConfig _gridConfig;
    
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
        
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        _t.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public override void StopMove()
    {
        base.StopMove();

        _rb.linearVelocity = Vector2.zero;
        _direction = Vector2.zero;
    }
}
