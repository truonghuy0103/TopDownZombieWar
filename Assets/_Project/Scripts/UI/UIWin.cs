using Core;
using UnityEngine;

public class UIWin : BaseUI
{
    public void ButtonHomeClicked()
    {
        UIManager.Instance.HideAllUI();
        LoadSceneManager.Instance.OnLoadScene("Main", (obj) =>
        {
            UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
        });
    }
}
