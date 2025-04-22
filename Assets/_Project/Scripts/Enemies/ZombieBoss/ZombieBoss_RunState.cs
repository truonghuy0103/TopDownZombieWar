using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ZombieBoss_RunState : FSMState
{
    [NonSerialized] public ZombieBossSystem parent;

    private int _randomSpeedUp;
    private float _timeSpeedUp = 3f;
    private float _timer;
    
    public override void OnEnter()
    {
        base.OnEnter();
        _randomSpeedUp = Random.Range(1, 3);;
        _timer = 0;
        //Play anim run
        parent.navMeshAgent.stoppingDistance = parent.radiusAttack;
        parent.navMeshAgent.speed = parent.configEnemyData.speed * _randomSpeedUp;
        parent.zombieBossDataBinding.Speed = parent.navMeshAgent.speed;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        parent.navMeshAgent.SetDestination(parent.player.position);
        
        _timer += Time.deltaTime;
        if (_timer >= _timeSpeedUp)
        {
            parent.navMeshAgent.speed = parent.configEnemyData.speed;
        }
        
        if (Vector3.Distance(parent.transform.position, parent.player.position) <= parent.radiusAttack)
        {
            parent.GotoState(parent.attackState);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        _timer = 0;
        parent.navMeshAgent.speed = parent.configEnemyData.speed;
    }
}
