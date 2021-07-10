using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(TextButtonSettings))]
public class TextButton : Button
{
    private TMP_Text text;
    private TextButtonSettings settings;

    private void SetDefault()
    {
        text = GetComponentInChildren<TMP_Text>();
        settings = GetComponent<TextButtonSettings>();

        text.font = settings.DefaultFont;
    }

    private void SetSelected()
    {
        text = GetComponentInChildren<TMP_Text>();
        settings = GetComponent<TextButtonSettings>();

        text.font = settings.SelectedFont;

        if(!EventSystem.current.alreadySelecting)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        switch (state)
        {
            case SelectionState.Normal:
                SetDefault();
                break;
            case SelectionState.Highlighted:
                SetSelected();
                break;
            case SelectionState.Pressed:
                break;
            case SelectionState.Selected:
                SetSelected();
                break;
            case SelectionState.Disabled:
                break;
            default:
                break;
        }
    }
}
