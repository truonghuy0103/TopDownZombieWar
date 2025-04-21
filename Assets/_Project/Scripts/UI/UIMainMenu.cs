using Core;
using UnityEngine;

public class UIMainMenu : BaseUI
{
    public void ButtonMissionOneClicked()
    {
        UIManager.Instance.HideUI(UIIndex.UIMainMenu);
        // Load scene
        ConfigMissionData configMissionData = ConfigManager.Instance.configMission.GetMissionDataById(1.ToString());
        LoadSceneManager.Instance.OnLoadScene(configMissionData.sceneName, (obj) =>
        {
            GameManager.Instance.SetupGameplay(1);
        });
    }
    
    public void ButtonMissionTwoClicked()
    {
        UIManager.Instance.HideUI(UIIndex.UIMainMenu);
        // Load scene
        ConfigMissionData configMissionData = ConfigManager.Instance.configMission.GetMissionDataById(1.ToString());
        LoadSceneManager.Instance.OnLoadScene(configMissionData.sceneName, (obj) =>
        {
            GameManager.Instance.SetupGameplay(2);
        });
    }
}
