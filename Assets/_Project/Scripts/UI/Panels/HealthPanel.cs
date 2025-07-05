using System.Collections.Generic;
using UnityEngine;

public class HealthPanel : MonoBehaviour
{
    [SerializeField] private Indicator _healthPrefab;
    [SerializeField] private Transform _healthsParent;

    private List<Indicator> _healthPoints;

    public void Init()
    {
        _healthPoints = new();
    }

    public void ActivateHealths(int count)
    {
        CheckPointsCount(count);

        foreach (var point in _healthPoints)
            point.ChangeActiveState(true);
    }
    public void RemoveHealth()
    {
        for (int i = _healthPoints.Count - 1; i >= 0; i--)
            if (_healthPoints[i].IsActive)
            {
                _healthPoints[i].ChangeActiveState(false);
                break;
            }
    }

    private void CreateHealths(int count)
    {
        Indicator point;
        for (int i = 0; i < count; i++)
        {
            point = InstantiateHealthPoint();
            _healthPoints.Add(point);
        }
    }

    private void CheckPointsCount(int count)
    {
        if (_healthPoints.Count < count)
        {
            CreateHealths(count - _healthPoints.Count);
        }
    }

    private Indicator InstantiateHealthPoint()
    {
        Indicator healthPoint = Instantiate(_healthPrefab, _healthsParent);
        healthPoint.Init();

        return healthPoint;
    }
}
