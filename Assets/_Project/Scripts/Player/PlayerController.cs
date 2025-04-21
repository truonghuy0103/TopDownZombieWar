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
