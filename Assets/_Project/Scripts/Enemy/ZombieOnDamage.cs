using System;
using UnityEngine;

public class ZombieOnDamage : MonoBehaviour
{
    [SerializeField] private ZombieSystem _zombieSystem;

    public void ApplyDamage(int damage)
    {
        _zombieSystem.OnDamage(damage);
    }
}
