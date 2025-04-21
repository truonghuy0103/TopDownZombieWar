using Core;
using UnityEngine;

public class UILose : BaseUI
{
    public void PlayAgainButtonClicked()
    {
        int currentMission = GameManager.Instance.currentMission;
        ConfigMissionData configMissionData = ConfigManager.Instance.configMission.GetMissionDataById(currentMission.ToString());
        
        UIManager.Instance.HideUI(this);
        
        LoadSceneManager.Instance.OnLoadScene("Main", (obj) =>
        {
            LoadSceneManager.Instance.OnLoadScene(configMissionData.sceneName, (obj) =>
            {
                GameManager.Instance.SetupGameplay(currentMission);
            });
        });
    }

    public void HomeButtonClicked()
    {
        UIManager.Instance.HideAllUI();
        LoadSceneManager.Instance.OnLoadScene("Main", (obj) =>
        {
            UIManager.Instance.ShowUI(UIIndex.UIMainMenu);
        });
    }
}
