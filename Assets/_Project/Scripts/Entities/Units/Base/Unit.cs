using UnityEngine;
using UnityEngine.Events;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Unit : MonoBehaviour, IMoving
{
    public event UnityAction<Collider2D> OnTriggerEnter;
    public event UnityAction<Collider2D> OnTriggerExit;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rb;
    [Header("Data")]
    [SerializeField, SerializeReference] private IMovement _movement;

    [Inject] private DiContainer _container;

    private void FixedUpdate()
    {
        _movement.Update();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter?.Invoke(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExit?.Invoke(collision);
    }

    public virtual void Init()
    {
        _movement.Init(_rb, transform);
        _container.BindInterfacesAndSelfTo<IMovement>().FromInstance(_movement);
        _container.Inject(_movement);
    }

    public virtual void Move(Vector2 direction)
    {
        _movement.Move(direction);
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
