using UnityEngine;
using UnityEngine.Events;

public class ArchiveScreen : Screen
{
    public event UnityAction OnMenuClicked;
    public event UnityAction OnWriteClicked;
    public event UnityAction OnCompressClicked;

    [Header("UI Elements")]
    [SerializeField] private TopPanel _topPanel;
    [SerializeField] private DiscPanel _discPanel;
    [SerializeField] private FilesPanel _filesPanel;

    public override void Init()
    {
        base.Init();

        _discPanel.OnWriteClicked += () => OnWriteClicked?.Invoke();
        _filesPanel.OnCompressClicked += () => OnCompressClicked?.Invoke();
    }

    public void ClickMenu()
    {
        OnMenuClicked?.Invoke();
    }
}
