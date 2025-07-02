using UnityEngine;

[System.Serializable]
public class Movement : IMovement
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _isStop;

    public void Move(Rigidbody2D rb, Vector2 direction)
    {
        if (_isStop) return;

        rb.AddForce(direction * _speed);
    }

    public void StartMove()
    {
        _isStop = false;
    }

    public void StopMove()
    {
        _isStop = true;
    }
}
