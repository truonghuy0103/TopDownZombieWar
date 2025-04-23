using Core;
using UnityEngine;

public class UIWin : BaseUI
{
    public override void OnSetUp(UIParam param = null)
    {
        base.OnSetUp(param);
        
        SoundManager.Instance.StopSoundBGM();
        SoundManager.Instance.StopAllSoundFX();
        
        LoadSceneManager.Instance.OnLoadScene("Main", (obj) =>
        {
            
        });
        
    }

    public void ButtonHomeClicked()
    {
        SoundManager.Instance.PlaySoundSFX(SoundFXIndex.Click);
        UIManager.Instance.HideAllUI();
        UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
    }
}
