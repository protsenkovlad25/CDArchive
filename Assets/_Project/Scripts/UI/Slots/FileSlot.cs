using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FileSlot : MonoBehaviour
{
    public UnityAction OnSelected;
    public UnityAction<FileSlot> OnClicked;

    [Header("File")]
    [SerializeField] private Image _background; 
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Color _simpleColor;
    [SerializeField] private Color _selectedColor;
    [Header("Indicators")]
    [SerializeField] private Indicator _indicatorPrefab;
    [SerializeField] private Transform _indicatorsParent;

    private bool _isSelected;

    private GameFileData _file;
    private CanvasGroup _canvasGroup;
    private List<Indicator> _indicators;

    public bool IsSelected => _isSelected;
    public GameFileData File => _file;

    public void Init()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _isSelected = false;

        InitIndicators();
    }
    
    private void InitIndicators()
    {
        _indicators = new();

        Indicator indicator;
        for (int i = 0; i < Configs.CompressionSetting.LevelsCount; i++)
        {
            indicator = Instantiate(_indicatorPrefab, _indicatorsParent);
            indicator.Init();

            _indicators.Add(indicator);
        }
    }
    private void ActivateIndicators(CompressionLevel compression)
    {
        for (int i = 0; i < _indicators.Count; i++)
        {
            _indicators[i].ChangeActiveState(i < (int)compression);
        }
    }

    public void SetFile(GameFileData file)
    {
        _file = file;

        UpdateData();
    }
    private void SetName(string name)
    {
        _nameText.text = name;
    }

    public void UpdateData()
    {
        SetName(_file.Name);
        ActivateIndicators(_file.CompressionLevel);
    }

    public void ChangeActiveState(bool state)
    {
        gameObject.SetActive(state);
    }    
    public void ChangeSelectedState(bool state)
    {
        _isSelected = state;
        _background.color = state ? _selectedColor : _simpleColor;

        if (state)
            OnSelected?.Invoke();
    }

    public void ClickFile()
    {
        OnClicked?.Invoke(this);
    }
}
