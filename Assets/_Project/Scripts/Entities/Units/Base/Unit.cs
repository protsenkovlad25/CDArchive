using UnityEngine;
using UnityEngine.Events;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Unit : MonoBehaviour, IMoving, IAttacking
{
    public event UnityAction<Unit, Collision2D> OnCollisionEnter;
    public event UnityAction<Unit, Collision2D> OnCollisionExit;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rb;
    [Header("Data")]
    [SerializeField, SerializeReference] private IMovement _movement;
    [SerializeField, SerializeReference] private IAttack _attack;

    [Inject] private DiContainer _container;

    public IAttack Attack => _attack;

    private void FixedUpdate()
    {
        _movement?.Update();
        _attack?.Update();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter?.Invoke(this, collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionExit?.Invoke(this, collision);
    }

    public virtual void Init()
    {
        InitAttack();
        InitMovement();
    }
    private void InitMovement()
    {
        if (_movement != null)
        {
            _movement.Init(_rb, transform);
            _container.BindInterfacesAndSelfTo<IMovement>().FromInstance(_movement);
            _container.Inject(_movement);
        }
    }
    private void InitAttack()
    {
        if (_attack != null)
        {
            _attack.Init(_rb, transform);
            _container.BindInterfacesAndSelfTo<IAttack>().FromInstance(_attack);
            _container.Inject(_attack);
        }
    }

    public void StartBehavior()
    {
        StartMove();
        StartAttack();
    }
    public void StopBehavior()
    {
        StopMove();
        StopAttack();
    }

    public virtual void Move(Vector2 direction)
    {
        _movement?.Move(direction);
    }
    public virtual void StartMove()
    {
        _movement?.StartMove();
    }
    public virtual void StopMove()
    {
        _movement?.StopMove();
    }

    public void Atack()
    {
        _attack?.Attack();
    }
    public void StartAttack()
    {
        _attack?.StartAttack();
    }
    public void StopAttack()
    {
        _attack?.StopAttack();
    }
}
