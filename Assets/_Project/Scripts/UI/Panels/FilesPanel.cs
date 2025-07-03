using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FilesPanel : Panel
{
    public event UnityAction OnCompressClicked;
    public event UnityAction<FileSlot> OnFileCliked;

    [Header("Files Data")]
    [SerializeField] private FileSlot _fileSlotPrefab;
    [SerializeField] private Transform _fileSlotsContainer;
    [Header("UI Elements")]
    [SerializeField] private BaseButton _compressButton;

    private List<FileSlot> _slots;

    public override void Init()
    {
        base.Init();

        _compressButton.Init();
        ChangeCompressInteract(false);
    }

    public void LoadFiles(List<GameFileData> files)
    {
        _slots = new();
        FileSlot newSlot;
        foreach (var file in files)
        {
            newSlot = InstantiateSlot();
            newSlot.SetFile(file);

            _slots.Add(newSlot);
        }
    }

    public void ChangeCompressInteract(bool state)
    {
        _compressButton.ChangeInteract(state);
    }

    public void ClickCompress()
    {
        OnCompressClicked?.Invoke();
    }
    public void ClickFile(FileSlot file)
    {
        OnFileCliked?.Invoke(file);
    }

    private FileSlot InstantiateSlot()
    {
        FileSlot slot = Instantiate(_fileSlotPrefab, _fileSlotsContainer);
        slot.OnClicked += ClickFile;
        slot.Init();

        return slot;
    }
}
