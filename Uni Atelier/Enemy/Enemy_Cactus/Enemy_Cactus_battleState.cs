using UnityEngine;

public class Enemy_Cactus_battleState : EnemyState
{
    protected Enemy_Cactus enemy;
    protected Transform player;

    private int presentHp;

    public Enemy_Cactus_battleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Cactus enemy)
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
