using UnityEngine;

public class GridBorder : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;

    public void SetColliderSize(Vector2 size)
    {
        _collider.size = size;
        _collider.offset = Vector2.zero;
    }
}
