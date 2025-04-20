using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class ZombieNormalSystem : ZombieSystem
{
    public ZombieNormal_IdleState idleState;
    public ZombieNormal_RunState runState;
    public ZombieNormal_AttackState attackState;
    public ZombieNormal_DeadState deadState;
    
    public NavMeshAgent navMeshAgent;
    public float radiusAttack = 2;
    public Transform target;
    
    public ZombieHealth zombieHealth;
    public ZombieNormalDataBinding zombieNormalDataBinding;

    public ConfigEnemyData configEnemyData;
    public override void OnSystemStart()
    {
        base.OnSystemStart();
        AddState(idleState);
        idleState.parent = this;
        
        AddState(runState);
        runState.parent = this;
        
        AddState(attackState);
        attackState.parent = this;
        
        AddState(deadState);
        deadState.parent = this;
    }

    public override void OnSetupZombie(object data)
    {
        base.OnSetupZombie(data);
        
        configEnemyData = data as ConfigEnemyData;
        navMeshAgent.speed = configEnemyData.speed;
        zombieHealth.SetupHP(configEnemyData.hp);
        damage = configEnemyData.damage;
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        GotoState(idleState);
    }

    public override void OnDamage(int damage)
    {
        base.OnDamage(damage);
        zombieHealth.TakeDamage(damage);
        if (zombieHealth.currentHp <= 0)
        {
            GotoState(deadState);
        }
    }
}
