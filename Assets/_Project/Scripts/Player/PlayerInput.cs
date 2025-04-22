using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInput : MonoBehaviour
{
    public Vector2 dir;
    public Vector2 dirFire;
    public event Action<bool> OnFire;
    private bool _isFire;
    
    public VariableJoystick joystickMove;
    public VariableJoystick joystickFire;

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
        Vector3 dirKeyboard = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 dirJoystick = joystickMove.Direction;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsFire = true;
        }
        else
        {
            IsFire = joystickFire.isFire;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            IsFire = false;
        }
        else
        {
            IsFire = joystickFire.isFire;
        }
        
        dir = dirKeyboard + dirJoystick;
        dirFire = joystickFire.Direction;
    }
}
