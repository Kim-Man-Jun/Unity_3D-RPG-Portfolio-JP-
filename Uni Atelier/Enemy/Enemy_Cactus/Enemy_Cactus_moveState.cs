using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy_Cactus_moveState : EnemyState
{
    protected Enemy_Cactus enemy;
    private float RandomX;
    private float RandomZ;

    public Enemy_Cactus_moveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Cactus enemy)
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.movingTime;

        RandomX = Random.Range(-1, 2);
        RandomZ = Random.Range(-1, 2);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(RandomX * enemy.moveSpeed, RandomZ * enemy.moveSpeed, enemy.rotateSpeed);
        enemy.transform.LookAt(enemy.transform);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (enemy.battleStart == true)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
