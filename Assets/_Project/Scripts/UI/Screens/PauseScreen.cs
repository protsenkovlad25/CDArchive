using UnityEngine;
using UnityEngine.Events;

public class PauseScreen : Screen
{
    public event UnityAction OnContinueClicked;
    public event UnityAction OnAbortClicked;

    [Header("UI Elements")]
    [SerializeField] private Transform _buttonsContainer;

    public override void Init()
    {
        base.Init();
    }

    public void ClickContinue()
    {
        OnContinueClicked?.Invoke();
    }
    public void ClickAbort()
    {
        OnAbortClicked?.Invoke();
    }
}
