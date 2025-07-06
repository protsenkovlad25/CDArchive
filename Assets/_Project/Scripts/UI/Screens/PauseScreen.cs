using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PauseScreen : Screen
{
    public event UnityAction OnContinueClicked;
    public event UnityAction OnAbortClicked;

    [Header("UI Elements")]
    [SerializeField] private Transform _buttonsContainer;

    public override void Init()
    {
        base.Init();

        _buttonsContainer.transform.localScale = Vector3.zero;
    }

    protected override void OpenAnim(UnityAction onEndAction = null)
    {
        gameObject.SetActive(true);
        _closeSeq?.Kill();

        _openSeq = DOTween.Sequence();
        _openSeq.SetUpdate(true);
        _openSeq.Append(_background.DOFade(_startAlpha, _openTime));
        _openSeq.Join(_buttonsContainer.DOScale(1, _openTime));
        _openSeq.AppendCallback(() =>
        {
            onEndAction?.Invoke();
            OnOpen();
        });
    }
    protected override void CloseAnim(UnityAction onEndAction = null)
    {
        _openSeq?.Kill();

        _closeSeq = DOTween.Sequence();
        _closeSeq.SetUpdate(true);
        _closeSeq.Append(_background.DOFade(0, _closeTime));
        _closeSeq.Join(_buttonsContainer.DOScale(0, _closeTime));
        _closeSeq.AppendCallback(() =>
        {
            gameObject.SetActive(false);
            onEndAction?.Invoke();
            OnClose();
        });
    }

    public void ClickContinue()
    {
        OnContinueClicked?.Invoke();
    }
    public void ClickAbort()
    {
        OnAbortClicked?.Invoke();
    }
}
