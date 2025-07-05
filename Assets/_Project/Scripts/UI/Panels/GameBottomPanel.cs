using UnityEngine;

public class GameBottomPanel : Panel
{
    [SerializeField] private HealthPanel _healthPanel;
    [SerializeField] private ProgressPanel _progressPanel;

    public override void Init()
    {
        base.Init();

        _healthPanel.Init();
        _progressPanel.Init();
    }

    public void UpdateProgress(float value)
    {
        _progressPanel.UpdateProgress(value);
    }

    public void ActivateHealths(int count)
    {
        _healthPanel.ActivateHealths(count);
    }
    public void RemoveHealth()
    {
        _healthPanel.RemoveHealth();
    }
}
