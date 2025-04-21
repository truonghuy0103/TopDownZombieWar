using UnityEngine;
using System;

[Serializable]
public class ZombieNormal_DeadState : FSMState
{
    [NonSerialized] public ZombieNormalSystem parent;

    private float _timer;
    public override void OnEnter()
    {
        base.OnEnter();
        parent.isDead = true;
        parent.zombieNormalDataBinding.Dead = true;
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
