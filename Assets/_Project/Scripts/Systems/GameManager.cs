using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    private int _currentMission;
    private PlayerController _playerController;
    private MissionControl _missionControl;
    public void SetupGameplay(int mission)
    {
        _currentMission = mission;
        
        Transform transCharacterControl = GameObject.Find("PlayerController").transform;
        _playerController = transCharacterControl.GetComponent<PlayerController>();

        Transform transMissionControl = GameObject.Find("MissionControl").transform;
        _missionControl = transMissionControl.GetComponent<MissionControl>();

        _playerController.OnSetupWeapon();
        _missionControl.OnSetupMission(mission);
    }

    public void StartGameAgain()
    {
        _playerController.OnSetupWeapon();
        _missionControl.OnSetupMission(_currentMission);
    }
}
