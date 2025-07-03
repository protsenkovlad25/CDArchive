using UnityEngine;
using Zenject;

public class ArchiveController : IInitializable
{
    private readonly InterfaceController interfaceController;

    private readonly ArchiveScreen archiveScreen;
    private FileSlot _selectedFile;

    public ArchiveController(InterfaceController interfaceController)
    {
        this.interfaceController = interfaceController;

        archiveScreen = interfaceController.Screens.ArchiveScreen;
    }

    public void Initialize()
    {
        archiveScreen.OnOpened += OpenArchive;
        archiveScreen.OnFileClicked += FileCliked;
        archiveScreen.OnCompressClicked += DeselectFile;
        archiveScreen.LoadFiles();

        _selectedFile = null;
    }

    private void OpenArchive()
    {

    }

    private void FileCliked(FileSlot file)
    {
        if (file != _selectedFile)
        {
            SelectFile(file);
        }
        else
        {
            DeselectFile();
        }
    }
    private void SelectFile(FileSlot file)
    {
        _selectedFile?.ChangeSelectedState(false);
        file.ChangeSelectedState(true);
        _selectedFile = file;

        archiveScreen.ChangeCompressInteract(true);
    }
    private void DeselectFile()
    {
        _selectedFile?.ChangeSelectedState(false);
        _selectedFile = null;

        archiveScreen.ChangeCompressInteract(false);
    }
}
