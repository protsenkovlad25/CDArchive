using UnityEngine;
using Zenject;

public class ArchiveController : IInitializable
{
    private readonly InterfaceController interfaceCntr;
    private readonly PlayerController playerCntr;

    private readonly ArchiveScreen archiveScreen;
    private FileSlot _selectedFile;

    public ArchiveController(InterfaceController interfaceCntr, PlayerController playerCntr)
    {
        this.interfaceCntr = interfaceCntr;
        this.playerCntr = playerCntr;

        archiveScreen = interfaceCntr.Screens.ArchiveScreen;
    }

    public void Initialize()
    {
        archiveScreen.OnOpened += OpenArchive;
        archiveScreen.OnFileClicked += FileCliked;
        archiveScreen.LoadFiles(playerCntr.PlayerData.GameFiles);

        _selectedFile = null;
    }

    private void OpenArchive()
    {

    }

    public void UpdateSelectedFileCompression(int compressionLvl)
    {
        _selectedFile.File.UpdateCompressionLevel(compressionLvl);
        _selectedFile.UpdateData();
        playerCntr.SaveData();
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
