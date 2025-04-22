using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerDataBinding _playerDataBinding;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private WeaponControl _weaponControl;
    [SerializeField] private float _speedMove = 2;
    [SerializeField] private int _maxHP = 20;
    [SerializeField] private Transform _transModel;
    
    public void OnSetupPlayer()
    {
        _playerHealth.Setup(_maxHP);
        _weaponControl.SetupEventWeapon();
        _weaponControl.SetupWeaponBehaviour();
    }
    
    private void Update()
    {
        Vector3 moveDir = new Vector3(_playerInput.dir.x, 0, _playerInput.dir.y);
        Vector3 fireDir = new Vector3(_playerInput.dirFire.x, 0, _playerInput.dirFire.y);

        if (fireDir.magnitude > 0)
        {
            fireDir.Normalize();
            _transModel.forward = fireDir;
        }
        
        if (moveDir.magnitude > 0)
        {
            moveDir.Normalize();
            //_transModel.forward = moveDir;
            _characterController.Move(moveDir * Time.deltaTime * _speedMove);
        }
        _playerDataBinding.Speed = moveDir.magnitude;
    }
}
