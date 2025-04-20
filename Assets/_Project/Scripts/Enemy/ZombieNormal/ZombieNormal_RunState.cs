using UnityEngine;
using System;

[Serializable]
public class ZombieNormal_RunState : FSMState
{
    [NonSerialized] public ZombieNormalSystem parent;

    private Transform _target;
    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        _target = (Transform)data;
        //Play anim run
        parent.navMeshAgent.stoppingDistance = parent.radiusAttack;
        parent.navMeshAgent.speed = parent.configEnemyData.speed;
        parent.zombieNormalDataBinding.Speed = parent.navMeshAgent.speed;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        parent.navMeshAgent.SetDestination(_target.position);
        if (Vector3.Distance(parent.transform.position, _target.position) <= parent.radiusAttack)
        {
            parent.GotoState(parent.attackState);
        }
    }
}
