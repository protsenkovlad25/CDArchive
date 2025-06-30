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
    [SerializeField] private Image _background;
    [SerializeField] private Image _lock;
    [Header("Base Anim Values")]
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
    #endregion

    #region Animations
    protected virtual void OpenAnim(UnityAction onEndAction = null)
    {
        gameObject.SetActive(true);

        onEndAction?.Invoke();
        OnOpened?.Invoke();
    }
    protected virtual void CloseAnim(UnityAction onEndAction = null)
    {
        gameObject.SetActive(false);

        onEndAction?.Invoke();
        OnClosed?.Invoke();
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
