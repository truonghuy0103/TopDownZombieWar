using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public event Action<int, int> OnHPChanged;
    private int _maxHP;
    private int _currentHP;

    public void Setup(int maxHP)
    {
        _maxHP = maxHP;
        _currentHP = _maxHP;
    }

    public void OnDamage(int damage)
    {
        _currentHP -= damage;
        if (OnHPChanged != null)
        {
            OnHPChanged.Invoke(_currentHP, _maxHP);
        }

        if (_currentHP <= 0)
        {
            //Player dead
        }
    }

}
