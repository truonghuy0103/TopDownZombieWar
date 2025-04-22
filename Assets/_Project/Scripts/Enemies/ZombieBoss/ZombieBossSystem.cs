using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class ZombieBossSystem : ZombieSystem
{
    public ZombieBoss_IdleState idleState;
    public ZombieBoss_RunState runState;
    public ZombieBoss_AttackState attackState;
    public ZombieBoss_DeadState deadState;
    
    public NavMeshAgent navMeshAgent;
    public float radiusAttack = 4;
    public Transform player;
    public PlayerHealth playerHealth;
    
    public ZombieHealth zombieHealth;
    public ZombieBossDataBinding zombieBossDataBinding;

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
        
        player = GameObject.FindWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            playerHealth = player.GetComponentInParent<PlayerHealth>();
        }
        
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
