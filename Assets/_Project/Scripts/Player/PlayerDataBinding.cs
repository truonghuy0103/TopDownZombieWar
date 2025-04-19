using System;
using UnityEngine;

public class PlayerDataBinding : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private int _animKeySpeed;
    private int _animKeyAttack;

    public float Speed
    {
        set => _animator.SetFloat(_animKeySpeed, value);
    }

    public bool Attack
    {
        set
        {
            if (value)
            {
                _animator.SetTrigger(_animKeyAttack);
            }
        }
    }

    private void Awake()
    {
        _animKeySpeed = Animator.StringToHash("Speed");
        _animKeyAttack = Animator.StringToHash("Attack");
    }
}
