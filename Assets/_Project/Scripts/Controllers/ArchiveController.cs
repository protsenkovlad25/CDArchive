using UnityEngine;

public class ArchiveController
{
    private readonly InterfaceController interfaceController;

    private ArchiveScreen _archiveScreen;
    private FileSlot _selectedFile;

    public ArchiveController(InterfaceController interfaceController)
    {
        this.interfaceController = interfaceController;
    }

    public void Init()
    {
        _archiveScreen = interfaceController.Screens.ArchiveScreen;
        _archiveScreen.OnOpened += OpenArchive;
        _archiveScreen.OnFileClicked += FileCliked;
        _archiveScreen.OnCompressClicked += DeselectFile;
        _archiveScreen.LoadFiles();
        
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

        _archiveScreen.ChangeCompressInteract(true);
    }
    private void DeselectFile()
    {
        _selectedFile?.ChangeSelectedState(false);
        _selectedFile = null;

        _archiveScreen.ChangeCompressInteract(false);
    }
}
