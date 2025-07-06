using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArchiveScreen : Screen
{
    public event UnityAction OnMenuClicked;
    public event UnityAction OnWriteClicked;
    public event UnityAction OnCompressClicked;
    public event UnityAction<FileSlot> OnFileClicked;

    [Header("UI Elements")]
    [SerializeField] private TopPanel _topPanel;
    [SerializeField] private DiscPanel _discPanel;
    [SerializeField] private FilesPanel _filesPanel;

    public override void Init()
    {
        base.Init();

        _discPanel.OnWriteClicked += () => OnWriteClicked?.Invoke();
        _filesPanel.OnFileCliked += (file) => OnFileClicked?.Invoke(file);
        _filesPanel.OnCompressClicked += () => OnCompressClicked?.Invoke();

        _topPanel.Init();
        _discPanel.Init();
        _filesPanel.Init();
    }

    public override void Open(UnityAction onEndAction = null)
    {
        base.Open(onEndAction);

        _topPanel.Open();
        _discPanel.Open();
        _filesPanel.Open();
    }
    public override void Close(UnityAction onEndAction = null)
    {
        base.Close(onEndAction);

        _topPanel.Close();
        _discPanel.Close();
        _filesPanel.Close();
    }

    public void LoadFiles(List<GameFileData> files)
    {
        _filesPanel.LoadFiles(files);
    }
    public void LoadDisc(DiscData data)
    {
        _discPanel.LoadDisc(data);
    }

    public void UpdateDiscSpace()
    {
        _discPanel.UpdateUsedSpace();
    }
    public void UpdateFileSpace(float fileSize, bool isAnim)
    {
        _discPanel.UpdateFileSpace(fileSize, isAnim);
    }

    public void ChangeCompressInteract(bool state)
    {
        _filesPanel.ChangeCompressInteract(state);
    }
    public void ChangeWriteInteract(bool state)
    {
        _discPanel.ChangeWriteInteract(state);
    }

    public void ClickMenu()
    {
        OnMenuClicked?.Invoke();
    }
}
