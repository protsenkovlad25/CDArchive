using UnityEngine;

[System.Serializable]
public abstract class Movement : IMovement
{
    [SerializeField] protected float _speed;
    [SerializeField] protected bool _isStop;

    protected Rigidbody2D _rb;
    protected Transform _t;
    protected Vector2 _direction;

    public virtual void Init(Rigidbody2D rb, Transform t)
    {
        _rb = rb;
        _t = t;
    }

    public abstract void Move(Vector2 direction);

    public virtual void Update()
    {
        if (_isStop) return;
    }

    public virtual void StartMove()
    {
        _isStop = false;
    }

    public virtual void StopMove()
    {
        _isStop = true;
    }
}
