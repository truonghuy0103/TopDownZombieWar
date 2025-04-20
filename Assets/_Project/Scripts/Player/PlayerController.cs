using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerDataBinding _playerDataBinding;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private WeaponControl _weaponControl;
    [SerializeField] private float _speedMove = 2;
    [SerializeField] private Transform _transModel;

    private bool _isFire;

    private void Awake()
    {
        ConfigManager.Instance.InitConfig(() =>
        {
            PoolDefine.Instance.InitPool(OnSetupWeapon);
            MissionControl.Instance.OnSetupMission();
        });
    }

    public void OnSetupWeapon()
    {
        _weaponControl.SetupWeaponBehaviour();
        _weaponControl.SwitchWeapon();
    }
    
    private void Update()
    {
        Vector3 moveDir = new Vector3(_playerInput.dir.x,0,_playerInput.dir.y);
        moveDir.Normalize();
        
        if (moveDir.magnitude > 0)
        {
            transform.forward = moveDir;
            _transModel.forward = moveDir;
            
            _characterController.Move(transform.forward * (Time.deltaTime * _speedMove));
        }
        _playerDataBinding.Speed = moveDir.magnitude;
    }
}
