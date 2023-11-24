using UnityEngine;

public class Enemy_Slime_battleState : EnemyState
{
    protected Enemy_Slime enemy;
    protected Transform player;

    private int presentHp;

    public Enemy_Slime_battleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Slime enemy)
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        enemy.ZeroVelocity();

        presentHp = (int)enemy.NowHp;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (presentHp != enemy.NowHp)
        {
            stateMachine.ChangeState(enemy.damagedState);
        }

        if (enemy.NowHp <= 0)
        {
            stateMachine.ChangeState(enemy.dieState);
        }
    }
}
