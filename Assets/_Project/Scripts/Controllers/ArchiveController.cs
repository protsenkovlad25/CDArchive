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
        archiveScreen.OnWriteClicked += WriteClicked;
        archiveScreen.LoadFiles(playerCntr.PlayerData.GameFiles);
        archiveScreen.LoadDisc(playerCntr.PlayerData.DiscData);

        _selectedFile = null;
    }

    private void OpenArchive()
    {
        DeselectFile();
    }

    public void UpdateSelectedFileCompression(int compressionLvl)
    {
        _selectedFile.File.UpdateCompressionLevel(compressionLvl);
        _selectedFile.UpdateData();
        playerCntr.SaveData();
    }

    private void WriteClicked()
    {
        playerCntr.WriteFileOnDisc(_selectedFile.File);
        archiveScreen.UpdateDiscSpace();
        _selectedFile.ChangeActiveState(false);

        DeselectFile();
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

        if (playerCntr.CanWriteFile(file.File))
            archiveScreen.ChangeWriteInteract(true);

        if (!playerCntr.IsDiscFilled())
            archiveScreen.UpdateFileSpace(file.File.Size, true);
        
        archiveScreen.ChangeCompressInteract(true);
    }
    private void DeselectFile()
    {
        archiveScreen.UpdateFileSpace(0, true);

        _selectedFile?.ChangeSelectedState(false);
        _selectedFile = null;

        archiveScreen.ChangeWriteInteract(false);
        archiveScreen.ChangeCompressInteract(false);
    }
}
