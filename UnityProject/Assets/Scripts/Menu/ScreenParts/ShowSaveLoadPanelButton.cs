using UnityEngine;

public class ShowSaveLoadPanelButton : ClickButton
{
    [SerializeField] private SaveLoadPanel panel;
    [SerializeField] private bool isSave;

    protected override void OnClick()
    {
        base.OnClick();

        if (panel)
        {
            panel.IsSave = isSave;
            panel.gameObject.SetActive(true);
        }
    }
}
