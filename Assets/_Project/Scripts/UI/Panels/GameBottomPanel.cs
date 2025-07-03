using UnityEngine;

public class GameBottomPanel : Panel
{
    [SerializeField] private ProgressPanel _progressPanel;

    public override void Init()
    {
        base.Init();

        _progressPanel.Init();
    }

    public void UpdateProgress(float value)
    {
        _progressPanel.UpdateProgress(value);
    }
}
