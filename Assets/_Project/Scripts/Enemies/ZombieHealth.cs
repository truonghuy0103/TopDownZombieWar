using UnityEngine;
using System;

public class ZombieHealth : MonoBehaviour
{
    public event Action<int, int> OnHPChanged;

    private int _maxHP;
    public int currentHp;

    public void SetupHP(int maxHP)
    {
        _maxHP = maxHP;
        currentHp = _maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }
}
