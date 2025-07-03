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

    public void UpdateProgress(float value)
    {
        _gameBottomPanel.UpdateProgress(value);
    }
}
