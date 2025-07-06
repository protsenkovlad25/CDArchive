using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuScreen : Screen
{
    public event UnityAction OnArchiveClicked;
    public event UnityAction OnQuitClicked;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text _topText;
    [SerializeField] private Transform _buttonsContainer;

    private List<Button> _buttons;

    public override void Init()
    {
        base.Init();

        _buttons = new();
        _buttons.AddRange(_buttonsContainer.GetComponentsInChildren<Button>());

        _topText.transform.localScale = Vector3.zero;
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
        _openSeq.Join(_topText.transform.DOScale(1, _openTime));
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
        _closeSeq.Join(_topText.transform.DOScale(0, _closeTime));
        _closeSeq.AppendCallback(() =>
        {
            gameObject.SetActive(false);
            onEndAction?.Invoke();
            OnClose();
        });
    }

    public void ClickArchive()
    {
        OnArchiveClicked?.Invoke();
    }
    public void ClickQuit()
    {
        OnQuitClicked?.Invoke();
    }
}
