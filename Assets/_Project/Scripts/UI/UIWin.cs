using Core;
using UnityEngine;

public class UIWin : BaseUI
{
    public override void OnSetUp(UIParam param = null)
    {
        base.OnSetUp(param);
        
        LoadSceneManager.Instance.OnLoadScene("Main", (obj) =>
        {
            
        });
        
    }

    public void ButtonHomeClicked()
    {
        UIManager.Instance.HideAllUI();
        UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
    }
}
