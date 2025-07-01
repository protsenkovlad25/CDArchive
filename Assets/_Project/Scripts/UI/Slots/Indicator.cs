using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Image _image;
    [Header("Active State")]
    [SerializeField] private Color _activeColor;
    [Header("Inactive State")]
    [SerializeField] private Color _inactiveColor;

    public void Init()
    {

    }

    public void ChangeActiveState(bool state)
    {
        _image.color = state ? _activeColor : _inactiveColor;
    }
}
