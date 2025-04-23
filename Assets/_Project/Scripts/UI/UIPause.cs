using Core;
using UnityEngine;

public class UIPause : BaseUI
{
    public override void OnSetUp(UIParam param = null)
    {
        base.OnSetUp(param);
        Time.timeScale = 0;
    }

    public void ResumeButtonClicked()
    {
        Time.timeScale = 1;
        UIManager.Instance.HideUI(this);
    }

    public void HomeButtonClicked()
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
