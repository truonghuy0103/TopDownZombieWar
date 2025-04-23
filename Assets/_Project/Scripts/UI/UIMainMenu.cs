using Core;
using UnityEngine;

public class UIMainMenu : BaseUI
{
    public void ButtonMissionOneClicked()
    {
        SoundManager.Instance.PlaySoundSFX(SoundFXIndex.Click);
        UIManager.Instance.HideUI(UIIndex.UIMainMenu);
        UIManager.Instance.ShowUI(UIIndex.UILoading);
        // Load scene
        ConfigMissionData configMissionData = ConfigManager.Instance.configMission.GetMissionDataById(1.ToString());
        LoadSceneManager.Instance.OnLoadScene(configMissionData.sceneName, (obj) =>
        {
            GameManager.Instance.SetupGameplay(1);
            UIManager.Instance.HideUI(UIIndex.UILoading);
            SoundManager.Instance.PlaySoundBGM(SoundBGM.MissionOne);
        });
    }
    
    public void ButtonMissionTwoClicked()
    {
        SoundManager.Instance.PlaySoundSFX(SoundFXIndex.Click);
        UIManager.Instance.HideUI(UIIndex.UIMainMenu);
        UIManager.Instance.ShowUI(UIIndex.UILoading);
        // Load scene
        ConfigMissionData configMissionData = ConfigManager.Instance.configMission.GetMissionDataById(2.ToString());
        LoadSceneManager.Instance.OnLoadScene(configMissionData.sceneName, (obj) =>
        {
            GameManager.Instance.SetupGameplay(2);
            UIManager.Instance.HideUI(UIIndex.UILoading);
            SoundManager.Instance.PlaySoundBGM(SoundBGM.MissionTwo);
        });
    }
}
