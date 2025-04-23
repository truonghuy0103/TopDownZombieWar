using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class ZombieBoss_IdleState : FSMState
{
    [NonSerialized] public ZombieBossSystem parent;

    private float _timeWait = 0;
    private float _randomTime = 2;

    public override void OnEnter()
    {
        base.OnEnter();
        SoundManager.Instance.PlaySoundZombie(parent.soundZombie);
        _randomTime = Random.Range(2f, 4f);
        _timeWait = 0;
        //Set anim idle
        parent.zombieBossDataBinding.Speed = 0;
        parent.navMeshAgent.speed = 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        _timeWait += Time.deltaTime;
        if (_timeWait >= _randomTime)
        {
            parent.GotoState(parent.runState);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        _timeWait = 0;
    }
}
