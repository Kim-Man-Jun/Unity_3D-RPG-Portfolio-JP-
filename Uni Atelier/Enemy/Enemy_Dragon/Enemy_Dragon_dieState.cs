using UnityEngine;

public class Enemy_Dragon_dieState : EnemyState
{
    protected Enemy_Dragon enemy;

    private float delayTime;

    public Enemy_Dragon_dieState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Dragon enemy)
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
            BattleSoundManager.instance.DragonDie();
            SaveLoadSystem.enemyDiecount++;
            enemy.enemyDie();
        }
    }
}
