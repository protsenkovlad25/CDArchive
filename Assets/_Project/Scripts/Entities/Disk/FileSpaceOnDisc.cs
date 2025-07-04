using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FileSpaceOnDisc : MonoBehaviour
{
    [SerializeField] private Image _spaceImage;
    [SerializeField] private RectTransform _textPanel;
    [SerializeField] private Color _canWriteColor;
    [SerializeField] private Color _notWriteColor;
    [SerializeField] private float _updateDuration;

    private float _discUISize;
    private RectTransform _rectT;

    public void Init()
    {
        _rectT = GetComponent<RectTransform>();

        UpdateSpace(0, 0, false, false);
    }

    public void SetDiscUISize(float size)
    {
        _discUISize = size;
    }

    public void ChangeTextPanelActive(bool state)
    {
        _textPanel.gameObject.SetActive(state);
    }

    public void UpdateSpace(float discUsedSpacePrecent, float fileSizePercent, bool isCanWrite, bool isAnim = true)
    {
        ChangeTextPanelActive(fileSizePercent > 0);

        _spaceImage.color = isCanWrite ? _canWriteColor : _notWriteColor;

        _rectT.DOAnchorPosY(_discUISize * discUsedSpacePrecent, isAnim ? _updateDuration : 0).SetEase(Ease.Linear);
        _rectT.DOSizeDelta(new(_rectT.sizeDelta.x, _discUISize * fileSizePercent), isAnim ? _updateDuration : 0).SetEase(Ease.Linear);
    }
}
