using UnityEngine;
using System;

[Serializable]
public class ZombieNormal_AttackState : FSMState
{
    [NonSerialized] public ZombieNormalSystem parent;

    private float _timer = 0;
    private float _timeLimit = 1;
    
    private Transform _target;

    public override void OnEnter()
    {
        base.OnEnter();
        
        _target = parent.target;
        //Anim attack
        parent.zombieNormalDataBinding.Attack = true;
        _timer = 0;
        _timeLimit = parent.configEnemyData.rateOfAttack;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        _timer += Time.deltaTime;
        if (_timer >= _timeLimit)
        {
            //Apply damage for player
            if (Vector3.Distance(parent.transform.position, _target.position) <= parent.radiusAttack)
            {
                parent.zombieNormalDataBinding.Attack = true;
                _timer = 0;
            }
            else
            {
                parent.GotoState(parent.idleState);
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        _timer = 0;
    }
}
