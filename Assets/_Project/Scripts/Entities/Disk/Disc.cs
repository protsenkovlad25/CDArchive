using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Disc : MonoBehaviour
{
    [SerializeField] private Image _greyDisk;
    [SerializeField] private Image _filledDisk;
    [SerializeField] private RectTransform _textPanel;
    [SerializeField] private FileSpaceOnDisc _fileSpace;
    [SerializeField] private float _updateDuration;

    private float _UISize;
    private float _lastSpace;

    private DiscData _data;
    private RectTransform _rectT;

    public void Init()
    {
        _rectT = GetComponent<RectTransform>();
        _UISize = _rectT.sizeDelta.x;

        _fileSpace.SetDiscUISize(_UISize);
        _fileSpace.Init();
    }

    public void SetData(DiscData data)
    {
        _data = data;

        _lastSpace = 0;
        UpdateUsedSpace(false);
    }

    public void UpdateFileSpace(float fileSize, bool isAnim)
    {
        Debug.Log("File size - " + fileSize);
        bool isCanWrite = _data.UsedSpace + fileSize <= _data.Capacity;

        _fileSpace.UpdateSpace(_data.UsedSpace / _data.Capacity, fileSize / _data.Capacity, isCanWrite, isAnim);
    }

    public void UpdateUsedSpace(bool isAnim = true)
    {
        if (isAnim)
        {
            StopCoroutine(nameof(ChangeFillAmount));
            StartCoroutine(nameof(ChangeFillAmount));
        }
        else
        {
            _lastSpace = _data.UsedSpace;
            _filledDisk.fillAmount = _lastSpace / _data.Capacity;
        }
        
        UpdateTextPanelPos(isAnim);
    }
    private void UpdateTextPanelPos(bool isAnim)
    {
        _textPanel.DOAnchorPosY(_UISize * _data.UsedSpace / _data.Capacity, isAnim ? _updateDuration : 0).SetEase(Ease.Linear);
    }

    private IEnumerator ChangeFillAmount()
    {
        float stepCount = 16;
        float spaceStep = (_data.UsedSpace - _lastSpace) / stepCount;
        for (int i = 0; i < stepCount; i++)
        {
            _lastSpace += spaceStep;
            _filledDisk.fillAmount = _lastSpace / _data.Capacity;

            yield return new WaitForSeconds(_updateDuration / stepCount);
        }
        yield break;
    }
}
