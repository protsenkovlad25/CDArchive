using UnityEngine;
using UnityEngine.Events;

public class GameScreen : Screen
{
    public event UnityAction OnPauseClicked;

    [Header("UI Elements")]
    [SerializeField] private TopPanel _topPanel;
    [SerializeField] private GamePanel _gamePanel;
    [SerializeField] private GameBottomPanel _gameBottomPanel;

    public override void Init()
    {
        base.Init();

        _gamePanel.OnPauseClicked += () => OnPauseClicked?.Invoke();

        _topPanel.Init();
        _gamePanel.Init();
        _gameBottomPanel.Init();
    }

    public override void Open(UnityAction onEndAction = null)
    {
        base.Open(onEndAction);

        _topPanel.Open(onEndAction);
        _gamePanel.Open(onEndAction);
        _gameBottomPanel.Open(onEndAction);
    }
    public override void Close(UnityAction onEndAction = null)
    {
        base.Close(onEndAction);

        _topPanel.Close(onEndAction);
        _gamePanel.Close(onEndAction);
        _gameBottomPanel.Close(onEndAction);
    }

    public void UpdateProgress(float value)
    {
        _gameBottomPanel.UpdateProgress(value);
    }

    public void ActivateHealths(int count)
    {
        _gameBottomPanel.ActivateHealths(count);
    }
    public void RemoveHealth()
    {
        _gameBottomPanel.RemoveHealth();
    }
}
