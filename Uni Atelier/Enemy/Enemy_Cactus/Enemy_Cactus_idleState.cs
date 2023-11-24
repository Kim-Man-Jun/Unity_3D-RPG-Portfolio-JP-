using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Cactus_idleState : EnemyState
{
    protected Enemy_Cactus enemy;
    protected Transform player;

    public Enemy_Cactus_idleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Cactus enemy)
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        enemy.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.moveState);
        }

        if (enemy.battleStart == true)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
