using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerDataBinding _playerDataBinding;

    public Vector2 dir;
    public event Action<bool> OnFire;
    private bool _isFire;

    public bool IsFire
    {
        get => _isFire;
        set
        {
            _isFire = value;
            if (OnFire != null)
            {
                OnFire.Invoke(value);
            }
        }
    }
    
    private void Update()
    { 
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsFire = true;
        }
    }
}
