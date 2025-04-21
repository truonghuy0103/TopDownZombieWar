using Core;
using UnityEngine;

public class UILose : BaseUI
{
    public void PlayAgainButtonClicked()
    {
        UIManager.Instance.HideUI(this);
        GameManager.Instance.StartGameAgain();
    }

    public void HomeButtonClicked()
    {
        UIManager.Instance.HideAllUI();
        UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
    }
}
