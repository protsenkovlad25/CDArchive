using UnityEngine;
using UnityEngine.Events;

public class DiscPanel : Panel
{
    public event UnityAction OnWriteClicked;

    public void ClickWrite()
    {
        OnWriteClicked?.Invoke();
    }
}
