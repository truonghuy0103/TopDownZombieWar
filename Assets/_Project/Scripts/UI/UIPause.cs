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
        SoundManager.Instance.PlaySoundSFX(SoundFXIndex.Click);
        UIManager.Instance.HideUI(this);
    }

    public void HomeButtonClicked()
    {
        Time.timeScale = 1;
        SoundManager.Instance.StopAllSoundFX();
        SoundManager.Instance.StopSoundBGM();
        SoundManager.Instance.PlaySoundSFX(SoundFXIndex.Click);
        UIManager.Instance.HideAllUI();
        UIManager.Instance.ShowUI(UIIndex.UILoading);
        LoadSceneManager.Instance.OnLoadScene("Main", (obj) =>
        {
            UIManager.Instance.HideUI(UIIndex.UILoading);
            UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
        });
    }
}
