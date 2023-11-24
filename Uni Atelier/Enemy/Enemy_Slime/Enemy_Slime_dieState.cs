using UnityEngine;

public class Enemy_Slime_dieState : EnemyState
{
    protected Enemy_Slime enemy;

    private float delayTime;

    public Enemy_Slime_dieState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Slime enemy)
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        delayTime = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        delayTime += Time.deltaTime;

        if (delayTime > 1)
        {
            BattleSoundManager.instance.SlimeDie();
            SaveLoadSystem.enemyDiecount++;
            enemy.enemyDie();
        }
    }
}
