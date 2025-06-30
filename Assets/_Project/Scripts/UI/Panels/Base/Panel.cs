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
                Direction.Left => -_rectTransform.sizeDelta.x,
                Direction.Right => _rectTransform.sizeDelta.x
            };
            float posY = _direction switch
            {
                Direction.Up => _rectTransform.sizeDelta.y,
                Direction.Down => -_rectTransform.sizeDelta.y,
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

        onEndAction?.Invoke();
    }
    protected virtual void CloseAnim(UnityAction onEndAction = null)
    {
        gameObject.SetActive(false);

        onEndAction?.Invoke();
    }
    #endregion

    #endregion
}
