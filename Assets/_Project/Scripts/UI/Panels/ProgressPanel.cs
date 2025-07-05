using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _changeDuration;

    public void Init()
    {
        _slider.value = 0;
    }

    public void UpdateProgress(float value, bool isAnim = true)
    {
        _slider.DOValue(value, isAnim ? _changeDuration : 0);
    }
}
