using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DiscPanel : Panel
{
    public event UnityAction OnWriteClicked;

    [SerializeField] private Disc _disc;
    [SerializeField] private TMP_Text _filledText;
    [SerializeField] private BaseButton _writeButton;

    private DiscData _discData;

    public override void Init()
    {
        base.Init();

        _disc.Init();
        _writeButton.Init();
    }

    public void LoadDisc(DiscData data)
    {
        _discData = data;

        _disc.SetData(data);
        UpdateFilledText();
    }

    public void UpdateUsedSpace()
    {
        _disc.UpdateUsedSpace();
        UpdateFilledText();
    }
    public void UpdateFileSpace(float fileSize, bool isAnim)
    {
        _disc.UpdateFileSpace(fileSize, isAnim);
    }
    public void UpdateFilledText()
    {
        _filledText.text = $"Filled by {(int)(_discData.UsedSpace / _discData.Capacity * 100)}%";
    }

    public void ChangeWriteInteract(bool state)
    {
        _writeButton.ChangeInteract(state);
    }

    public void ClickWrite()
    {
        OnWriteClicked?.Invoke();
    }
}
