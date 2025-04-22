using Core;
using UnityEngine;

public class UILose : BaseUI
{
    public void PlayAgainButtonClicked()
    {
        int currentMission = GameManager.Instance.currentMission;
        ConfigMissionData configMissionData = ConfigManager.Instance.configMission.GetMissionDataById(currentMission.ToString());
        
        UIManager.Instance.HideUI(this);
        UIManager.Instance.ShowUI(UIIndex.UILoading);
        
        LoadSceneManager.Instance.OnLoadScene("Main", (obj) =>
        {
            LoadSceneManager.Instance.OnLoadScene(configMissionData.sceneName, (obj) =>
            {
                GameManager.Instance.SetupGameplay(currentMission);
                UIManager.Instance.HideUI(UIIndex.UILoading);
            });
        });
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
