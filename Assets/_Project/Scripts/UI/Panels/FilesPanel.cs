using UnityEngine;
using UnityEngine.Events;

public class FilesPanel : Panel
{
    public event UnityAction OnCompressClicked;

    public void ClickCompress()
    {
        OnCompressClicked?.Invoke();
    }
}
