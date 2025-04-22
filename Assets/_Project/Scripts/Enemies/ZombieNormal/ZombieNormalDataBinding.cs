using System;
using UnityEngine;

public class ZombieNormalDataBinding : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _animKeyAttack;
    private int _animKeySpeed;
    private int _animKeyDead;

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

    public bool Dead
    {
        set
        {
            if (value)
            {
                _animator.SetTrigger(_animKeyDead);
            }
        }
    }

    private void Awake()
    {
        _animKeySpeed = Animator.StringToHash("Speed");
        _animKeyAttack = Animator.StringToHash("Attack");
        _animKeyDead = Animator.StringToHash("Dead");
    }
}
