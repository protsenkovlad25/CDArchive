using System.Collections.Generic;
using UnityEngine;
using VP;
using Zenject;

public enum EnemyType { None, ShootEnemy, DirectEnemy }

public class EnemiesController : IInitializable
{
    private readonly Transform startEnemiesParent;
    private readonly Transform activeEnemiesParent;

    [Inject] private DiContainer _container;

    private EnemiesSettingsConfig _enemiesConfig;
    private Dictionary<EnemyType, Pool<Unit>> _poolEnemyByType;
    private List<Unit> _activeEnemies;

    public EnemiesController(Transform startEnemiesParent, Transform activeEnemiesParent)
    {
        this.startEnemiesParent = startEnemiesParent;
        this.activeEnemiesParent = activeEnemiesParent;
    }

    public void Initialize()
    {
        _activeEnemies = new();
        _enemiesConfig = Configs.EnemiesSettings;

        InitPools();
    }
    private void InitPools()
    {
        _poolEnemyByType = new();

        Pool<Unit> poolEnemy;
        poolEnemy = new Pool<Unit>(_enemiesConfig.GetPrefab(EnemyType.ShootEnemy), startEnemiesParent, 5);
        poolEnemy.OnCreateNew += NewEnemy;
        _poolEnemyByType.Add(EnemyType.ShootEnemy, poolEnemy);

        foreach (var enemy in poolEnemy.ObjectsList)
            NewEnemy(enemy);

        poolEnemy = new Pool<Unit>(_enemiesConfig.GetPrefab(EnemyType.DirectEnemy), startEnemiesParent, 5);
        poolEnemy.OnCreateNew += NewEnemy;
        _poolEnemyByType.Add(EnemyType.DirectEnemy, poolEnemy);

        foreach (var enemy in poolEnemy.ObjectsList)
            NewEnemy(enemy);
    }
    private void NewEnemy(Unit enemy)
    {
        _container.BindInterfacesAndSelfTo<Unit>().FromInstance(enemy);
        _container.Inject(enemy);
        
        enemy.OnCollisionEnter += EnemyCollision;
        enemy.Init();
    }

    public Unit SpawnEnemy(EnemyType type, Vector2 pos)
    {
        Unit enemy = _poolEnemyByType[type].Take();
        enemy.transform.parent = activeEnemiesParent;
        enemy.transform.position = pos;
        enemy.StopBehavior();

        _activeEnemies.Add(enemy);

        return enemy;
    }
    public void StartEnemies()
    {
        foreach (var enemy in _activeEnemies)
            enemy.StartBehavior();
    }
    public void StopEnemies()
    {
        foreach (var enemy in _activeEnemies)
            enemy.StopBehavior();
    }

    public void DeactiveAllEnemies()
    {
        for (int i = _activeEnemies.Count - 1; i >= 0; i--)
            DeactiveEnemy(_activeEnemies[i]);
    }
    private void DeactiveEnemy(Unit enemy)
    {
        _activeEnemies.Remove(enemy);

        foreach (var pool in _poolEnemyByType.Values)
            if (pool.ObjectsList.Contains(enemy))
            {
                pool.Return(enemy);
                break;
            }
    }

    private void EnemyCollision(Unit enemy, Collision2D collision)
    {
        if (!collision.collider.TryGetComponent<Unit>(out _))
            DeactiveEnemy(enemy);
    }
}
