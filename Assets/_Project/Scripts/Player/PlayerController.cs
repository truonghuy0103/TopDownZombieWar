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
        Vector3 moveDir = new Vector3(_playerInput.dir.x, 0, _playerInput.dir.y);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 999))
        {
            Vector3 target = hitInfo.point;
            target.y = _transModel.position.y;
            _transModel.LookAt(target);
        }
        
        if (moveDir.magnitude > 0)
        {
            moveDir.Normalize();
            //_transModel.forward = moveDir;
            _characterController.Move(moveDir * (Time.deltaTime * _speedMove));
        }
        _playerDataBinding.Speed = moveDir.magnitude;
    }
}
