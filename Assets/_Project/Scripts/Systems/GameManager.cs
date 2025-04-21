using UnityEngine;
using System;
using UnityEngine.Serialization;

public class GameManager : SingletonMono<GameManager>
{
    public int currentMission;
    private PlayerController _playerController;
    private MissionControl _missionControl;
    private UIInGame _uiInGame;
    
    public void SetupGameplay(int mission)
    {
        currentMission = mission;
        
        Transform transCharacterControl = GameObject.Find("PlayerController").transform;
        _playerController = transCharacterControl.GetComponent<PlayerController>();

        Transform transMissionControl = GameObject.Find("MissionControl").transform;
        _missionControl = transMissionControl.GetComponent<MissionControl>();
        
        Transform transUIInGame = GameObject.Find("UIInGame").transform;
        _uiInGame = transUIInGame.GetComponent<UIInGame>();

        PoolDefine.Instance.InitPool(() =>
        {
            _playerController.OnSetupPlayer();
        });
        
        _missionControl.OnSetupMission(mission);
        _uiInGame.SetupUIInGame();
    }
}
