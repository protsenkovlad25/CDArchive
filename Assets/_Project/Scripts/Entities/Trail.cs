using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;

    public void Clear()
    {
        _trail.Clear();
    }
}
