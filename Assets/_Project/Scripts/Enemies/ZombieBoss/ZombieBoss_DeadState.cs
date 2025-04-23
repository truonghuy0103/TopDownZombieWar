using System;
using UnityEngine;

[Serializable]
public class ZombieBoss_DeadState : FSMState
{
    [NonSerialized] public ZombieBossSystem parent;

    private float _timer;
    public override void OnEnter()
    {
        base.OnEnter();
        
        SoundManager.Instance.StopSoundZombie(parent.soundZombie);
        
        parent.isDead = true;
        parent.zombieBossDataBinding.Dead = true;
        _timer = 1;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            parent.OnDead();
        }
    }
}
