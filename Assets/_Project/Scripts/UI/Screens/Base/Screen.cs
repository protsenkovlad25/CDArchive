using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Screen : MonoBehaviour
{
    public event UnityAction OnOpened;
    public event UnityAction OnClosed;

    #region Serialize Fields
    [Header("Base UI")]
    [SerializeField] protected Image _background;
    [SerializeField] protected Image _lock;
    [Header("Base Anim Times")]
    [SerializeField] protected float _openTime;
    [SerializeField] protected float _closeTime;
    #endregion

    #region Fields
    protected float _startAlpha;

    protected Sequence _openSeq;
    protected Sequence _closeSeq;
    protected RectTransform _rectTransform;
    #endregion

    #region Methods
    public virtual void Init()
    {
        _rectTransform = GetComponent<RectTransform>();

        _startAlpha = _background.color.a;
        _background.DOFade(0, 0);
    }

    #region OpenClose
    public virtual void Open(UnityAction onEndAction = null)
    {
        OpenAnim(onEndAction);
    }
    public virtual void Close(UnityAction onEndAction = null)
    {
        CloseAnim(onEndAction);
    }
    protected virtual void OnOpen()
    {
        OnOpened?.Invoke();
    }
    protected virtual void OnClose()
    {
        OnClosed?.Invoke();
    }
    #endregion

    #region Animations
    protected virtual void OpenAnim(UnityAction onEndAction = null)
    {
        gameObject.SetActive(true);
        _closeSeq?.Kill();

        _openSeq = DOTween.Sequence();
        _openSeq.SetUpdate(true);
        _openSeq.Append(_background.DOFade(_startAlpha, _openTime));
        _openSeq.AppendCallback(() =>
        {
            onEndAction?.Invoke();
            OnOpen();
        });
    }
    protected virtual void CloseAnim(UnityAction onEndAction = null)
    {
        _openSeq?.Kill();

        _closeSeq = DOTween.Sequence();
        _closeSeq.SetUpdate(true);
        _closeSeq.Append(_background.DOFade(0, _closeTime));
        _closeSeq.AppendCallback(() =>
        {
            gameObject.SetActive(false);
            onEndAction?.Invoke();
            OnClose();
        });
    }
    #endregion

    #region Lock
    public void LockPanel()
    {
        _lock.gameObject.SetActive(true);
    }
    public void UnlockPanel()
    {
        _lock.gameObject?.SetActive(false);
    }
    #endregion

    #endregion
}
