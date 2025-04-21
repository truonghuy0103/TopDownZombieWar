using Core;
using UnityEngine;

public class UIWin : BaseUI
{
    public void ButtonHomeClicked()
    {
        UIManager.Instance.HideUI(this);
        UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
    }
}
