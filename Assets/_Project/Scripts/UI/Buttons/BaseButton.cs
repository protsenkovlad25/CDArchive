using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BaseButton : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    public virtual void Init()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ChangeInteract(bool state)
    {
        _canvasGroup.interactable = state;
    }
}
