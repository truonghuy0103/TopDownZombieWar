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
    
    public bool isDead = false;
    public override void OnSystemStart()
    {
        base.OnSystemStart();
        
        idleState.parent = this;
        AddState(idleState);
        
        runState.parent = this;
        AddState(runState);
        
        attackState.parent = this;
        AddState(attackState);
        
        deadState.parent = this;
        AddState(deadState);
    }

    public override void OnSetupZombie(object data)
    {
        base.OnSetupZombie(data);
        
        target = GameObject.FindWithTag("Player").transform;
        
        configEnemyData = data as ConfigEnemyData;
        navMeshAgent.speed = configEnemyData.speed;
        zombieHealth.SetupHP(configEnemyData.hp);
        damage = configEnemyData.damage;
        
        isDead = false;
    }

    public override void OnDamage(int damage)
    {
        base.OnDamage(damage);
        zombieHealth.TakeDamage(damage);
        if (zombieHealth.currentHp <= 0 && !isDead)
        {
            GotoState(deadState);
        }
    }

    public void ResetZombie()
    {
        zombieHealth.SetupHP(configEnemyData.hp);
        isDead = false;
        GotoState(idleState);
    }
}
