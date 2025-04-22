using UnityEngine;
using System;
using Core;

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
        
        Transform blood = PoolManager.Instance.dictPools[NamePool.PoolBloodPlayer.ToString()].GetObjectInstance();
        if (blood != null)
        {
            blood.position = transform.position;
            blood.forward = transform.forward;
        }
        
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
