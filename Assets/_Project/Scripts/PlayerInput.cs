using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerDataBinding _playerDataBinding;
    
    private void Update()
    {
        Vector2  dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _playerDataBinding.Speed = dir.magnitude;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerDataBinding.Attack = true;
        }
    }
}
