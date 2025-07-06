using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public abstract class Panel : MonoBehaviour
{
    protected enum Direction { Center, Up, Down, Left, Right }

    #region Serialize Fields
    [Header("Anim Values")]
    [SerializeField] protected Direction _direction;
    [SerializeField] protected float _openTime;
    [SerializeField] protected float _closeTime;
    #endregion

    #region Fields
    protected Vector2 _openPos;
    protected Vector2 _closePos;

    protected Sequence _openSeq;
    protected Sequence _closeSeq;
    protected RectTransform _rectTransform;
    #endregion

    #region Methods
    public virtual void Init()
    {
        _rectTransform = GetComponent<RectTransform>();
            
        InitStartPosition();
    }

    protected virtual void InitStartPosition()
    {
        if (_direction != Direction.Center)
        {
            _openPos = _rectTransform.anchoredPosition;

            float posX = _direction switch
            {
                Direction.Up => _rectTransform.anchoredPosition.x,
                Direction.Down => _rectTransform.anchoredPosition.x,
                Direction.Left => -_rectTransform.rect.size.x,
                Direction.Right => _rectTransform.rect.size.x
            };
            float posY = _direction switch
            {
                Direction.Up => _rectTransform.rect.size.y,
                Direction.Down => -_rectTransform.rect.size.y,
                Direction.Left => _rectTransform.anchoredPosition.y,
                Direction.Right => _rectTransform.anchoredPosition.y
            };

            _closePos = new Vector2(posX, posY);
            _rectTransform.anchoredPosition = _closePos;
        }
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
        _closeSeq?.Kill();

        if (_direction == Direction.Center)
            ScaleOpenAnim(onEndAction);
        else
            MoveOpenAnim(onEndAction);
    }
    private void MoveOpenAnim(UnityAction onEndAction = null)
    {
        _openSeq = DOTween.Sequence();
        _openSeq.Append(_rectTransform.DOAnchorPos(_openPos, _openTime));
        _openSeq.AppendCallback(() => onEndAction?.Invoke());
    }
    private void ScaleOpenAnim(UnityAction onEndAction = null)
    {
        _openSeq = DOTween.Sequence();
        _openSeq.Append(transform.DOScale(1, _openTime));
        _openSeq.AppendCallback(() => onEndAction?.Invoke());
    }
    
    protected virtual void CloseAnim(UnityAction onEndAction = null)
    {
        _openSeq?.Kill();

        if (_direction == Direction.Center)
            ScaleCloseAnim(onEndAction);
        else
            MoveCloseAnim(onEndAction);
    }
    private void MoveCloseAnim(UnityAction onEndAction = null)
    {
        _closeSeq = DOTween.Sequence();
        _closeSeq.Append(_rectTransform.DOAnchorPos(_closePos, _closeTime));
        _closeSeq.AppendCallback(() =>
        {
            gameObject.SetActive(false);
            onEndAction?.Invoke();
        });
    }
    private void ScaleCloseAnim(UnityAction onEndAction = null)
    {
        _closeSeq = DOTween.Sequence();
        _closeSeq.Append(transform.DOScale(0, _closeTime));
        _closeSeq.AppendCallback(() =>
        {
            gameObject.SetActive(false);
            onEndAction?.Invoke();
        });
    }
    #endregion

    #endregion
}
