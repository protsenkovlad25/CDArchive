using UnityEngine;
using UnityEngine.Events;

public class GamePanel : Panel
{
    public event UnityAction OnPauseClicked;

    private void Update()
    {
        if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.P))
            ClickPause();
    }

    public void ClickPause()
    {
        OnPauseClicked?.Invoke();
    }
}
