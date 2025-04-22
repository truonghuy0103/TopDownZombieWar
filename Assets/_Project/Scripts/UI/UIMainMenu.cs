using Core;
using UnityEngine;

public class UIMainMenu : BaseUI
{
    public void ButtonMissionOneClicked()
    {
        UIManager.Instance.HideUI(UIIndex.UIMainMenu);
        UIManager.Instance.ShowUI(UIIndex.UILoading);
        // Load scene
        ConfigMissionData configMissionData = ConfigManager.Instance.configMission.GetMissionDataById(1.ToString());
        LoadSceneManager.Instance.OnLoadScene(configMissionData.sceneName, (obj) =>
        {
            GameManager.Instance.SetupGameplay(1);
            UIManager.Instance.HideUI(UIIndex.UILoading);
        });
    }
    
    public void ButtonMissionTwoClicked()
    {
        UIManager.Instance.HideUI(UIIndex.UIMainMenu);
        UIManager.Instance.ShowUI(UIIndex.UILoading);
        // Load scene
        ConfigMissionData configMissionData = ConfigManager.Instance.configMission.GetMissionDataById(1.ToString());
        LoadSceneManager.Instance.OnLoadScene(configMissionData.sceneName, (obj) =>
        {
            GameManager.Instance.SetupGameplay(2);
            UIManager.Instance.HideUI(UIIndex.UILoading);
        });
    }
}
