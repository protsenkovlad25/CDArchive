using UnityEngine;
using Zenject;

[System.Serializable]
public class EnemyShootAttack : IAttack
{
    [SerializeField] private float _shootInterval;

    [Inject] private PlayerController _playerCntr;
    [Inject] private EnemiesController _enemiesCntr;

    private float _delay;
    private bool _isStop;
    private Rigidbody2D _rb;
    private Transform _t;

    public float ShootInterval { get => _shootInterval; set => _shootInterval = value; }

    public void Init(Rigidbody2D rb, Transform t)
    {
        _rb = rb;
        _t = t;
    }

    public void Attack()
    {
        if (_isStop) return;

        SpawnDirectionEnemy();
    }
    private void SpawnDirectionEnemy()
    {
        Unit dirEnemy = _enemiesCntr.SpawnEnemy(EnemyType.DirectEnemy, _t.position);
        dirEnemy.Move((_playerCntr.Player.transform.position - _t.position).normalized);
        dirEnemy.StartBehavior();
    }

    public void StartAttack()
    {
        _isStop = false;
        _delay = _shootInterval;
    }
    public void StopAttack()
    {
        _isStop = true;
    }

    public void Update()
    {
        if (_isStop) return;

        if (_delay <= 0)
        {
            _delay = _shootInterval;
            Attack();
        }
        else _delay -= Time.deltaTime;
    }
}
