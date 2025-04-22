using UnityEngine;
using System;

[Serializable]
public class ZombieNormal_RunState : FSMState
{
    [NonSerialized] public ZombieNormalSystem parent;
    
    public override void OnEnter()
    {
        base.OnEnter();
        //Play anim run
        parent.navMeshAgent.stoppingDistance = parent.radiusAttack;
        parent.navMeshAgent.speed = parent.configEnemyData.speed;
        parent.zombieNormalDataBinding.Speed = parent.navMeshAgent.speed;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        parent.navMeshAgent.SetDestination(parent.player.position);
        if (Vector3.Distance(parent.transform.position, parent.player.position) <= parent.radiusAttack)
        {
            parent.GotoState(parent.attackState);
        }
    }
}
