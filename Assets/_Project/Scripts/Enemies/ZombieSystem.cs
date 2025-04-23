using System;
using UnityEngine;

public class ZombieSystem : FSMSystem
{
    public event Action<ZombieSystem> OnZombieDead;
    public SoundFXIndex soundZombie;
    public int damage;
    public virtual void OnSetupZombie(object data)
    {
       
    }

    public virtual void OnDamage(int damage)
    {
        
    }

    public virtual void OnDead()
    {
        if (OnZombieDead != null)
        {
            OnZombieDead.Invoke(this);
        }
        //Destroy(gameObject);
    }
}
