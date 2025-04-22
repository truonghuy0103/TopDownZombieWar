using Core;
using UnityEngine;

public class UIWin : BaseUI
{
    public void ButtonHomeClicked()
    {
        UIManager.Instance.HideAllUI();
        UIManager.Instance.ShowUI(UIIndex.UILoading);
        LoadSceneManager.Instance.OnLoadScene("Main", (obj) =>
        {
            UIManager.Instance.HideUI(UIIndex.UILoading);
            UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
        });
    }
}
