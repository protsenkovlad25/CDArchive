using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Image _image;
    [Header("Active State")]
    [SerializeField] private Color _activeColor;
    [Header("Inactive State")]
    [SerializeField] private Color _inactiveColor;

    private bool _isActive;

    public bool IsActive => _isActive;

    public void Init()
    {
        _isActive = false;
    }

    public void ChangeActiveState(bool state)
    {
        _isActive = state;
        _image.color = state ? _activeColor : _inactiveColor;
    }
}
