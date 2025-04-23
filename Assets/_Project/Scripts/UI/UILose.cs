using Core;
using UnityEngine;

public class UILose : BaseUI
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

    public void PlayAgainButtonClicked()
    {
        int currentMission = GameManager.Instance.currentMission;
        ConfigMissionData configMissionData = ConfigManager.Instance.configMission.GetMissionDataById(currentMission.ToString());
        
        UIManager.Instance.HideUI(this);
        UIManager.Instance.ShowUI(UIIndex.UILoading);
        
        LoadSceneManager.Instance.OnLoadScene(configMissionData.sceneName, (obj) =>
        {
            GameManager.Instance.SetupGameplay(currentMission);
            UIManager.Instance.HideUI(UIIndex.UILoading);
            SoundManager.Instance.PlaySoundBGM((SoundBGM)currentMission - 1,1,true);
        });
    }

    public void HomeButtonClicked()
    {
        SoundManager.Instance.PlaySoundSFX(SoundFXIndex.Click);
        
        UIManager.Instance.HideAllUI();
        UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
    }
}
