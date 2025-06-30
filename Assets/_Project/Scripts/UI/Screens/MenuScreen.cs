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
